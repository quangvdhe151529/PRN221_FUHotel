using BusinessObject.Models;
using DataAccess.ControllerDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        public List<RoomType> GetAll()
        => RoomTypeDAO.Instance.GetAll();
    }
}
