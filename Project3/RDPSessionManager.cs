﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using RDPCOMAPILib;

namespace RDPSessionManager
{
    public class RDPSessionManager
    {
        public static  RDPSession currentSession = null;
        public static object _lock = new object();
        
        private static int countControlClient=0;
        private static int countViewClient = 0;

        private static bool isSessionAlive = false;

        private static string controlString = null;

        public bool IsSessionAlive {
            get { return isSessionAlive; }
        }

        public void createSession()  //made it non-static
        {
            lock ( _lock ) {

                if ( null == currentSession ) {

                    currentSession = new RDPSession();
                    currentSession.Open();

                }

            }


        }


        private static void connectWithControl()
        {
            //Console.WriteLine("connecting control");

            currentSession.OnAttendeeConnected -= incomingControl;
            currentSession.OnAttendeeDisconnected -= outgoingControl;
            currentSession.OnAttendeeConnected += incomingControl;
            currentSession.OnAttendeeDisconnected += outgoingControl;

            try
            {
                currentSession.Resume();
                isSessionAlive = true;
            }
            catch(Exception e) {
                Console.WriteLine("the error is in opening connection: "+e);
                throw e;
            }

            //session.Open();
        }


        private static void connectWithView()
        {
            Console.WriteLine("connecting View");

            currentSession.OnAttendeeConnected -= incomingView;
            currentSession.OnAttendeeDisconnected -= outgoingView;
            currentSession.OnAttendeeConnected += incomingView;
            currentSession.OnAttendeeDisconnected += outgoingView;

            try {

                currentSession.Resume();
                isSessionAlive = true;
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
                lock ( this ) {

                    if ( null == currentSession )
                        return;

                    isSessionAlive = false;

                    countControlClient = 0;
                    countViewClient = 0;
                    ReleaseEventHandlers();

                    currentSession.Pause();
                    
                }
            }
            catch (Exception Ex)
            {
               // MessageBox.Show("Error Connecting", "Error connecting to remote desktop " + " Error:  " + Ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("error disconnecting" + Ex);
                throw Ex;
            }

        }

        private static void ReleaseEventHandlers() {

            if ( null == currentSession )
                return;
            currentSession.OnAttendeeConnected -= incomingControl;
            currentSession.OnAttendeeDisconnected -= outgoingControl;

            currentSession.OnAttendeeConnected -= incomingView;
            currentSession.OnAttendeeDisconnected -= outgoingView;

        }

        public void destroy() {

            try {
                lock ( this ) {

                    if ( null == currentSession )
                        return;

                    ReleaseEventHandlers();
                    currentSession.Close();
                    currentSession = null;
                    controlString = null;

                    isSessionAlive = false;

                    countControlClient = 0;
                    countViewClient = 0; 

                }
            } catch ( Exception Ex ) {
                // MessageBox.Show("Error Connecting", "Error connecting to remote desktop " + " Error:  " + Ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine( "error disconnecting" + Ex );
                throw Ex;
            }

        }




        private string connectionString(RDPSession session, String authString, string group, string password, int clientLimit)
        {
            IRDPSRAPIInvitation invitation = session.Invitations.CreateInvitation(authString, group, password, clientLimit);
            return invitation.ConnectionString;
        }


        private static void incomingControl(object Guest)
        {
            countControlClient++;

            IRDPSRAPIAttendee MyGuest = ( IRDPSRAPIAttendee ) Guest;
            MyGuest.ControlLevel = CTRL_LEVEL.CTRL_LEVEL_INTERACTIVE;


            /*
            CResolution res1 = new CResolution();
            DEVMODE1 dm1 = res1.getCurrentResolution();
            DEVMODE1 dm2 = res1.getSupportedResolutionList()[0];
            
            //String a = Console.ReadLine();
            res1.setSupportedResolution(dm2);
            res1.setSupportedResolution(dm1);
            //String abn = Console.ReadLine();
             * */


           // Console.WriteLine("connected with control: " + countControlClient);

        }

        private static void outgoingControl(object Guest)
        {
            countControlClient--;


           // Console.WriteLine("connected with control: " + countControlClient);


        }
        

        private static void incomingView(object Guest)
        {
            countViewClient++;
            
            IRDPSRAPIAttendee MyGuest = (IRDPSRAPIAttendee)Guest;
            MyGuest.ControlLevel = CTRL_LEVEL.CTRL_LEVEL_VIEW;

            Console.WriteLine("connected with view: " + countViewClient);


        }

        private static void outgoingView(object Guest)
        {
            countViewClient--;

            Console.WriteLine("connected with view: " + countViewClient);


        }

        /// <summary>
        /// throws exception
        /// </summary>
        public void shareControl()
        {
            lock ( this ) {

                if ( !isSessionAlive ) {

                    createSession();
                    connectWithControl();

                } else {

                    throw new SessionExistsException();//"Without disconnecting,you can not create another one");
                }
            }
            
        }

        /// <summary>
        /// throws exception
        /// </summary>
        public void shareView()
        {
            if ( !isSessionAlive )
            {
                createSession();
                connectWithView();
                isSessionAlive = true;
            }
            else
            {
                throw new SessionExistsException();
            }
        }



        public string getInvitationString( int clientLimit ) {

            if ( null == controlString )
                controlString = getUnprotectedInvitationString(clientLimit);

            return controlString;

        }
        public String getUnprotectedInvitationString(int clientLimit)
        {
            //Console.WriteLine("the invitation string is being generated: \n" );

            String invitationString = connectionString(currentSession, "Test", "Group", "", clientLimit);
           // Console.WriteLine("the invitation string: \n" + invitationString);

            return invitationString;
        
        }

        public String getProtectedInvitationString(int clientLimit,String password)
        {
            String invitationString = connectionString(currentSession, "Test", "Group", password, clientLimit);
           // Console.WriteLine("the invitation string: \n" + invitationString);

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

        public string GetControlString() {


            try {

                if ( isSessionAlive ) {

                    return getInvitationString( 5 ); //the max no of client 
                }

                this.disconnect();
                this.shareControl();
                return getInvitationString( 5 ); //the max no of client 

            } catch ( Exception e ) {

                throw e;

            }
        }
        public String forceNewSession()
        {
            //currentSession = null;
            isSessionAlive = false;

            //

            createSession();

            connectWithControl();
            //this.shareControl();
            return getInvitationString(5); //the max no of client 

        }

    }
    


}
