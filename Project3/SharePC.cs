using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using RDPCOMAPILib;

namespace SharePc
{
    class SharePC
    {
        public static RDPSession currentSession = null;
        
        private static int countControlClient=0;
        private static int countViewClient = 0;        

        public static void createSession()
        {
            currentSession = new RDPSession();
        }


        private static void connectWithControl()//RDPSession session)
        {
            Console.WriteLine("connecting control");
            currentSession.OnAttendeeConnected += incomingControl;

            try
            {
                currentSession.Open();
            }
            catch(Exception e) {
                Console.WriteLine("the error is in opening connection: "+e);            
            }

            //session.Open();
        }


        private static void connectWithView()//RDPSession session)
        {
            Console.WriteLine("connecting View");
            currentSession.OnAttendeeConnected += incomingView;

            try
            {
                currentSession.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("the error is in opening connection: " + e);

            }

            //session.Open();
        }


        public void disconnect()//RDPSession session)
        {
            try
            {
                currentSession.Close();

                countControlClient = 0;
                countViewClient = 0;
            }
            catch (Exception Ex)
            {
               // MessageBox.Show("Error Connecting", "Error connecting to remote desktop " + " Error:  " + Ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("error disconnecting"+Ex);
            }

        }

        
        private string connectionString(RDPSession session, String authString,string group, string password, int clientLimit)
        {
            IRDPSRAPIInvitation invitation = session.Invitations.CreateInvitation (authString, group, password, clientLimit);
            return invitation.ConnectionString;
        }

        private static void incomingControl(object Guest)
        {
            countControlClient++;

            IRDPSRAPIAttendee MyGuest = (IRDPSRAPIAttendee)Guest;
            MyGuest.ControlLevel = CTRL_LEVEL.CTRL_LEVEL_INTERACTIVE;

            Console.WriteLine("connected with control: "+ countControlClient);

        }

        

        private static void incomingView(object Guest)
        {
            countViewClient++;
            
            IRDPSRAPIAttendee MyGuest = (IRDPSRAPIAttendee)Guest;
            MyGuest.ControlLevel = CTRL_LEVEL.CTRL_LEVEL_VIEW;

            Console.WriteLine("connected with view: " + countControlClient);


        }

        public void shareControl()
        {
            createSession();
            connectWithControl();//currentSession);
           

        }

        public void shareView()
        {
            createSession();
            connectWithView();//currentSession);
        }

        public String getInvitationString(int clientLimit)
        {
            String invitationString = connectionString(currentSession, "Test", "Group", "", clientLimit);
            Console.WriteLine("the invitation string: \n" + invitationString);

            return invitationString;
        
        }

        public String getProtectedInvitationString(int clientLimit,String password)
        {
            String invitationString = connectionString(currentSession, "Test", "Group", password, clientLimit);
            Console.WriteLine("the invitation string: \n" + invitationString);

            return invitationString;

        }

        public int getControlClientNumber()
        {
            return countControlClient;
        }

        public int getViewClientNumber()
        {
            return countViewClient;
        }



    }
}
