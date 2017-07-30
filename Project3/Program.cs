using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDPSessionManager
{
     class Program
    {       
         static void Main()
         {
          //   Console.WriteLine("hello");

             RDPSessionManager pc1 = new RDPSessionManager();             
             
             //pc1.shareControl();
             //String viewInvitation = pc1.getInvitationString(5); //the max no of client 
             //Console.WriteLine("for control:\n"+viewInvitation);

             //pc1.disconnect();
             
             //pc1.shareControl();
             //String controlInvitation = pc1.getInvitationString( ( int ) 2 );
             //Console.WriteLine("for control:\n"+controlInvitation);


             String invitation1 = pc1.GetControlString();
             Console.WriteLine(invitation1);

         /*    pc1.disconnect();

             String invitation2 = pc1.GetControlString();

             Console.WriteLine("INVITATION 2222:\n" + invitation2);

             String a = Console.ReadLine();

             try
             {
                 String aa = pc1.GetControlString();
                 Console.WriteLine(aa);
                // pc1.disconnect();
             }
             catch (Exception e)
             {
                 Console.WriteLine("error:" + e.Message);
                 try
                 {
                     pc1.destroy();
                 }
                 catch (Exception e1)
                 {
                     Console.WriteLine("error1:" + e1.Message);
                 }
                 try
                 {
                     pc1.forceNewSession();
                 }
                 catch (Exception e2)
                 {
                     Console.WriteLine("error2:" + e2.Message);
                 }

             }
             String invitation3 = pc1.GetControlString();

             Console.WriteLine("invitaion3:\n"+invitation3);

             String bs = Console.ReadLine();

             System.Diagnostics.Debug.Print( invitation1 );

             pc1.destroy();*/

             String b = Console.ReadLine();
           //  while (true) ;

             
         }
        
    }
}
