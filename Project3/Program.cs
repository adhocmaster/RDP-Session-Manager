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

             SharePC pc1 = new SharePC();

             SharePC pc2 = new SharePC();
             //pc.shareControl();
             
             pc1.shareView();
             String viewInvitation = pc1.getInvitationString(16); //the max no of client 
             Console.WriteLine("for view:\n"+viewInvitation);

             pc2.shareControl();
             String controlInvitation = pc2.getInvitationString(16);
             Console.WriteLine("for control:\n"+controlInvitation);
             String a = Console.ReadLine();
             

             
         }
        
    }
}
