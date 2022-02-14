using DiplomnaStudentApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Services
{
    public interface IRoomService
    {
        void UpdateRoom(RoomDto roomDto);
        RoomDto GetRoom(string roomName);
        List<string> GetRoomsNames();
        void UpdateRoomImage(byte [] roomImage, string roomName);
    }
}
