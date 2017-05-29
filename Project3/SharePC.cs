using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using RDPCOMAPILib;

namespace SharePc
{
    public class SharePC
    {
        public static RDPSession currentSession = null;
        public static object _lock = new object();
        
        private static int countControlClient=0;
        private static int countViewClient = 0;        

        public static void createSession()
        {
            lock ( _lock ) {
                
                if ( null == currentSession )
                    currentSession = new RDPSession();
            }
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
                throw e;
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
                throw e;

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
                throw Ex;
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

        /// <summary>
        /// throws exception
        /// </summary>
        public void shareControl()
        {
            createSession();
            connectWithControl();//currentSession);
           

        }

        /// <summary>
        /// throws exception
        /// </summary>
        public void shareView()
        {
            createSession();
            connectWithView();//currentSession);
        }



        internal string getInvitationString( int clientLimit ) {
            
            return getUnprotectedInvitationString( clientLimit );

        }
        public String getUnprotectedInvitationString(int clientLimit)
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
