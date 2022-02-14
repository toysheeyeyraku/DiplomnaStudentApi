using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Model;
using DiplomnaStudentApi.Repository;
using DiplomnaStudentApi.Services;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.ServicesImpl
{
    public class RoomService: IRoomService
    {
        private IRoomManager roomManager;
        private IStudentProfileManager studentProfileManager;

        public RoomService(IRoomManager roomManager, IStudentProfileManager studentProfileManager)
        {
            this.roomManager = roomManager;
            this.studentProfileManager = studentProfileManager;
        }

        public void UpdateRoom(RoomDto roomDto)
        {
            RoomUpdateValidation(roomDto, roomDto.Name);
            Room room = roomManager.GetFirstOrDefault((roomIterator) => roomIterator.Name.Equals(roomDto.Name));
            bool isExist = true;
            if (room == null)
            {
                room = new Room();
                room.Id = roomDto.Name;
                isExist = false;
            }
            room.Name = roomDto.Name;
            room.Capacity = roomDto.Capacity;
            room.UserIds = roomDto.UserIds;
            
            if (isExist)
            {
                roomManager.Update(room.Id, room);
            }
            else
            {
                roomManager.Add(room);
            }
            UpdateStudentProfileForRoom(room);


        }

        public RoomDto GetRoom(string roomName)
        {
            Room room = roomManager.GetFirstOrDefault((r) => r.Name.Equals(roomName));
            if (room == null)
            {
                return new RoomDto();
            }
            RoomDto roomDto = new RoomDto();
            roomDto.Id = room.Id;
            roomDto.UserIds = room.UserIds;
            roomDto.Name = room.Name;
            roomDto.RoomImage = room.RoomImage;
            roomDto.Capacity = room.Capacity;
            return roomDto;
        }

        public List<string> GetRoomsNames()
        {
            return roomManager.GetAll().Select(r => r.Name).ToList();
        }

        public void UpdateRoomImage(byte[] roomImage, string roomName)
        {
            Room room = roomManager.GetFirstOrDefault((r) => r.Name.Equals(roomName));
            room.RoomImage = roomImage;
            roomManager.Update(room.Id, room);
        }

        private void UpdateStudentProfileForRoom(Room room)
        {
            foreach (var userId in room.UserIds)
            {
                StudentProfile user = studentProfileManager.GetById(userId);
                user.Room = room.Id;
                studentProfileManager.Update(user.Id, user);
            }
        }

        private void RoomUpdateValidation(RoomDto roomDto, string roomId)
        {
            if (roomDto.Capacity < roomDto.UserIds.Count)
            {
                throw new Exception("Room Capacity is too small");
            }
            foreach(var userId in roomDto.UserIds)
            {
                StudentProfile user = studentProfileManager.GetById(userId);
                if (user == null)
                {
                    throw new Exception("user not exist");
                }
                if (user.Room != null && !user.Room.Equals(roomId))
                {
                    throw new Exception("User already have other room");
                }
            }
        }

        
    }
}
