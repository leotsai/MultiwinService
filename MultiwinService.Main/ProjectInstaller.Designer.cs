namespace MultiwinService.Main
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.mainServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // mainServiceProcessInstaller
            // 
            this.mainServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.NetworkService;
            this.mainServiceProcessInstaller.Password = null;
            this.mainServiceProcessInstaller.Username = null;
            // 
            // mainServiceInstaller
            // 
            this.mainServiceInstaller.ServiceName = "Zdit Miaozhuan MainService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.mainServiceProcessInstaller,
            this.mainServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller mainServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller mainServiceInstaller;
    }
}