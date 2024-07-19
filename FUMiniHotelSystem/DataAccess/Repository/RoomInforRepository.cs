using BusinessObject.Models;
using DataAccess.ControllerDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class RoomInforRepository : IRoomInforRepository
    {
        public List<RoomInformation> GetAll()
        => RoomInforDAO.Instance.GetAll();  
    }
}
