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
             pc.shareControl();

             String temp = pc.getInvitationString();
             Console.WriteLine("in main: \n"+temp);

             String a = Console.ReadLine();
             

             
         }
        
    }
}
