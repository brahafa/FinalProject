using Clicker.Classes;
using Clicker.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicker.BL
{
    public class DisplayBL
    {

        public static DisplayDAL displatDAL;


        public DisplayBL()
        {
            displatDAL = new DisplayDAL();
        }

        public void AddNewDisplay(int Id, int IdQuestion)
        {
            displatDAL.AddNewDisplay(Id, IdQuestion);
        }
        public void deleteDisplayByIdQuestion(int IdQuestion)
        {
            displatDAL.deleteDisplayByIdQuestion(IdQuestion);
        }

        public int maxIdDisplay()
        {
            return displatDAL.maxIdDisplay();
        }

        public List<Display> getDisplayByIdQuestion(int IdQuestion)
        {
            return displatDAL.getDisplayByIdQuestion(IdQuestion);
        }

    }
}