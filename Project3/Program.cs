using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePc
{
     class Program
    {
       
         static void Main()
         {
             Console.WriteLine("hello");

             SharePC pc = new SharePC();
             //pc.shareControl();
             pc.shareView();

             String temp = pc.getInvitationString(16); //the max no of client 
             Console.WriteLine("in main: \n"+temp);

             String a = Console.ReadLine();
             

             
         }
        
    }
}
