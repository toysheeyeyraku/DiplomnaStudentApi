
using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Services;
using DiplomnaStudentApi.Student.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Admin.Controller
{
    public class AdminRoomController : ApiController
    {
        private IRoomService roomService;
        public AdminRoomController(IRoomService roomService)
        {
            this.roomService = roomService;
        }

        [Authorize(Roles = "admin")]
        [Route("admin/room")]
        [HttpGet]
        public RoomDto GetRoom([FromQuery(Name = "roomName")] string roomName)
        {
            return roomService.GetRoom(roomName);
        }

        [Authorize(Roles = "admin")]
        [Route("admin/room")]
        [HttpPost]
        public string UpdateRoom([FromBody] RoomDto roomDto)
        {
            roomService.UpdateRoom(roomDto);
            return "ok";
        }

        [Authorize(Roles = "admin")]
        [Route("admin/room/all")]
        [HttpGet]
        public List<string> GetRooms()
        {
            return roomService.GetRoomsNames();
        }

        [Authorize(Roles = "admin")]
        [Route("admin/room/updateImage")]
        [HttpPost]
        public string UpdateImage([FromQuery(Name = "roomName")] string roomName)
        {
            byte[] roomImage = base.GetImageFromRequestBytes();
            roomService.UpdateRoomImage(roomImage, roomName);
            return "ok";
        }
    }
}
