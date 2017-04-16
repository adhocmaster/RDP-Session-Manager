using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RDPCOMAPILib;

namespace SharePc
{
    class SharePC
    {
        public static RDPSession currentSession = null;

        public static void createSession()
        {
            currentSession = new RDPSession();
        }

        public static void Connect(RDPSession session)
        {
            session.OnAttendeeConnected += Incoming;
            session.Open();
        }

        public static void Disconnect(RDPSession session)
        {
            try
            {
                session.Close();
            }
            catch (Exception Ex)
            {
               // MessageBox.Show("Error Connecting", "Error connecting to remote desktop " + " Error:  " + Ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("error disconnecting"+Ex);
            }

        }

        public static string getConnectionString(RDPSession session, String authString,
            string group, string password, int clientLimit)
        {
            IRDPSRAPIInvitation invitation =
                session.Invitations.CreateInvitation
                (authString, group, password, clientLimit);
            return invitation.ConnectionString;
        }

        private static void Incoming(object Guest)
        {
            IRDPSRAPIAttendee MyGuest = (IRDPSRAPIAttendee)Guest;
            MyGuest.ControlLevel = CTRL_LEVEL.CTRL_LEVEL_INTERACTIVE;
        }

        public void shareControl()
        {
            createSession();
            Connect(currentSession);
           // String invitationString = getConnectionString(currentSession,
           //     "Test", "Group", "", 16);
           // Console.WriteLine("the invitation string:in share control \n"+invitationString);

            //return invitationString;

        }

        public String getInvitationString()
        {
            String invitationString = getConnectionString(currentSession,
                "Test", "Group", "", 16);
            Console.WriteLine("the invitation string: \n" + invitationString);

            return invitationString;
        
        }
        



    }
}
