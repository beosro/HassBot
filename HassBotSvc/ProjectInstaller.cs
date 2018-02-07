///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/06/2018
//  FILE            : ProjectInstaller.cs
//  DESCRIPTION     : Installer Component. Look into 
//                    ProjectInstaller.Designer.cs for authentication info.
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace HassBotSvc {
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer {
        public ProjectInstaller() {
            InitializeComponent();
        }
    }
}
