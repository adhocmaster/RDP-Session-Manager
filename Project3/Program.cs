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
             String viewInvitation = pc.getUnprotectedInvitationString(16); //the max no of client 
             Console.WriteLine("for view:\n"+viewInvitation);

             String temp = pc.getInvitationString( 16 ); //the max no of client  similar to getUnprotectedInvitationString
             Console.WriteLine( "in main: \n" + temp ); 

             pc.shareControl();
             String controlInvitation = pc.getUnprotectedInvitationString(16);
             Console.WriteLine("for control:\n"+controlInvitation);
             String a = Console.ReadLine();
             

             
         }
        
    }
}
