namespace Controles_Auxiliares
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            gbxConn = new GroupBox();
            lblDisconnected = new Label();
            lblConnected = new Label();
            lblStatus = new Label();
            btnDisconnect = new Button();
            btnConnect = new Button();
            gbxActions = new GroupBox();
            btnMemberShip = new Button();
            btnStartLogging = new Button();
            btnDoor = new Button();
            btnFinger = new Button();
            btnDelete = new Button();
            btnCreate = new Button();
            gbxConn.SuspendLayout();
            gbxActions.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.ForeColor = SystemColors.ControlDarkDark;
            lblTitle.Location = new Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(312, 22);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Controles Auxiliares de la Puerta";
            // 
            // gbxConn
            // 
            gbxConn.Controls.Add(lblDisconnected);
            gbxConn.Controls.Add(lblConnected);
            gbxConn.Controls.Add(lblStatus);
            gbxConn.Controls.Add(btnDisconnect);
            gbxConn.Controls.Add(btnConnect);
            gbxConn.Font = new Font("Arial", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            gbxConn.ForeColor = SystemColors.ControlDarkDark;
            gbxConn.Location = new Point(12, 41);
            gbxConn.Name = "gbxConn";
            gbxConn.Size = new Size(767, 100);
            gbxConn.TabIndex = 1;
            gbxConn.TabStop = false;
            gbxConn.Text = "Conexión";
            // 
            // lblDisconnected
            // 
            lblDisconnected.AutoSize = true;
            lblDisconnected.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblDisconnected.ForeColor = Color.IndianRed;
            lblDisconnected.Location = new Point(330, 48);
            lblDisconnected.Name = "lblDisconnected";
            lblDisconnected.Size = new Size(146, 19);
            lblDisconnected.TabIndex = 2;
            lblDisconnected.Text = "DESCONECTADO";
            // 
            // lblConnected
            // 
            lblConnected.AutoSize = true;
            lblConnected.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblConnected.ForeColor = Color.YellowGreen;
            lblConnected.Location = new Point(330, 48);
            lblConnected.Name = "lblConnected";
            lblConnected.Size = new Size(112, 19);
            lblConnected.TabIndex = 2;
            lblConnected.Text = "CONECTADO";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblStatus.Location = new Point(255, 48);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(69, 19);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Estado:";
            // 
            // btnDisconnect
            // 
            btnDisconnect.BackColor = Color.LightCoral;
            btnDisconnect.Location = new Point(132, 32);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(108, 47);
            btnDisconnect.TabIndex = 0;
            btnDisconnect.Text = "Desconectar";
            btnDisconnect.UseVisualStyleBackColor = false;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // btnConnect
            // 
            btnConnect.BackColor = Color.FromArgb(112, 204, 244);
            btnConnect.Location = new Point(18, 32);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(108, 47);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "Conectar";
            btnConnect.UseVisualStyleBackColor = false;
            btnConnect.Click += btnConnect_Click;
            // 
            // gbxActions
            // 
            gbxActions.Controls.Add(btnMemberShip);
            gbxActions.Controls.Add(btnStartLogging);
            gbxActions.Controls.Add(btnDoor);
            gbxActions.Controls.Add(btnFinger);
            gbxActions.Controls.Add(btnDelete);
            gbxActions.Controls.Add(btnCreate);
            gbxActions.Font = new Font("Arial", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            gbxActions.ForeColor = SystemColors.ControlDarkDark;
            gbxActions.Location = new Point(12, 160);
            gbxActions.Name = "gbxActions";
            gbxActions.Size = new Size(767, 100);
            gbxActions.TabIndex = 1;
            gbxActions.TabStop = false;
            gbxActions.Text = "Conexión";
            // 
            // btnMemberShip
            // 
            btnMemberShip.Location = new Point(634, 33);
            btnMemberShip.Name = "btnMemberShip";
            btnMemberShip.Size = new Size(117, 47);
            btnMemberShip.TabIndex = 1;
            btnMemberShip.Text = "Sincronizar membresía";
            btnMemberShip.UseVisualStyleBackColor = true;
            btnMemberShip.Click += btnMemberShip_Click;
            // 
            // btnStartLogging
            // 
            btnStartLogging.Location = new Point(510, 33);
            btnStartLogging.Name = "btnStartLogging";
            btnStartLogging.Size = new Size(117, 47);
            btnStartLogging.TabIndex = 1;
            btnStartLogging.Text = "Iniciar Monitoreo";
            btnStartLogging.UseVisualStyleBackColor = true;
            btnStartLogging.Click += btnStartLogging_Click;
            // 
            // btnDoor
            // 
            btnDoor.Location = new Point(387, 33);
            btnDoor.Name = "btnDoor";
            btnDoor.Size = new Size(117, 47);
            btnDoor.TabIndex = 1;
            btnDoor.Text = "Abrir Puerta";
            btnDoor.UseVisualStyleBackColor = true;
            btnDoor.Click += btnDoor_Click;
            // 
            // btnFinger
            // 
            btnFinger.Location = new Point(264, 33);
            btnFinger.Name = "btnFinger";
            btnFinger.Size = new Size(117, 47);
            btnFinger.TabIndex = 1;
            btnFinger.Text = "Tomar/Registrar huella";
            btnFinger.UseVisualStyleBackColor = true;
            btnFinger.Click += btnFinger_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(141, 33);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(117, 47);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "Sincronizar Eliminación";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(18, 33);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(117, 47);
            btnCreate.TabIndex = 1;
            btnCreate.Text = "Sincronizar Creación";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(791, 280);
            Controls.Add(gbxActions);
            Controls.Add(gbxConn);
            Controls.Add(lblTitle);
            MaximizeBox = false;
            Name = "Form1";
            Text = "NF Auxiliares";
            Load += Form1_Load;
            gbxConn.ResumeLayout(false);
            gbxConn.PerformLayout();
            gbxActions.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private GroupBox gbxConn;
        private Label lblConnected;
        private Label lblStatus;
        private Button btnDisconnect;
        private Button btnConnect;
        private Label lblDisconnected;
        private GroupBox gbxActions;
        private Button btnCreate;
        private Button btnStartLogging;
        private Button btnDoor;
        private Button btnFinger;
        private Button btnDelete;
        private Button btnMemberShip;
    }
}