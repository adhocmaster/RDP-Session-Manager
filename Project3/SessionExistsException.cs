using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePc {

    public class SessionExistsException : ApplicationException {
        public SessionExistsException()
            : base( "Without disconnecting,you can not create another one" ) {

        }
        public SessionExistsException( String msg )
            : base( msg ) {

        }

    }

}
