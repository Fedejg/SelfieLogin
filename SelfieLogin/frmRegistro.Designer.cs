namespace SelfieLogin
{
    partial class frmRegistro
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnTakeSelfie = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.gbDatosAcceso = new System.Windows.Forms.GroupBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.lblAlias = new System.Windows.Forms.Label();
            this.lblSelfie = new System.Windows.Forms.Label();
            this.txtAlias = new System.Windows.Forms.TextBox();
            this.pbSelfie = new System.Windows.Forms.PictureBox();
            this.pbImagen = new System.Windows.Forms.PictureBox();
            this.gbDatosAcceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelfie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(126, 41);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(134, 20);
            this.txtUsuario.TabIndex = 0;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(126, 80);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(134, 20);
            this.txtPass.TabIndex = 1;
            // 
            // btnTakeSelfie
            // 
            this.btnTakeSelfie.BackColor = System.Drawing.SystemColors.Control;
            this.btnTakeSelfie.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTakeSelfie.Location = new System.Drawing.Point(6, 155);
            this.btnTakeSelfie.Name = "btnTakeSelfie";
            this.btnTakeSelfie.Size = new System.Drawing.Size(114, 23);
            this.btnTakeSelfie.TabIndex = 3;
            this.btnTakeSelfie.Text = "Tomar Imagen";
            this.btnTakeSelfie.UseVisualStyleBackColor = false;
            this.btnTakeSelfie.Click += new System.EventHandler(this.btnTakeSelfie_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancelar.Location = new System.Drawing.Point(442, 238);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(150, 23);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar Registro";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // gbDatosAcceso
            // 
            this.gbDatosAcceso.Controls.Add(this.btnGuardar);
            this.gbDatosAcceso.Controls.Add(this.lblUsuario);
            this.gbDatosAcceso.Controls.Add(this.lblPass);
            this.gbDatosAcceso.Controls.Add(this.lblAlias);
            this.gbDatosAcceso.Controls.Add(this.lblSelfie);
            this.gbDatosAcceso.Controls.Add(this.txtAlias);
            this.gbDatosAcceso.Controls.Add(this.pbSelfie);
            this.gbDatosAcceso.Controls.Add(this.txtUsuario);
            this.gbDatosAcceso.Controls.Add(this.txtPass);
            this.gbDatosAcceso.Controls.Add(this.btnTakeSelfie);
            this.gbDatosAcceso.ForeColor = System.Drawing.Color.White;
            this.gbDatosAcceso.Location = new System.Drawing.Point(332, 12);
            this.gbDatosAcceso.Name = "gbDatosAcceso";
            this.gbDatosAcceso.Size = new System.Drawing.Size(266, 191);
            this.gbDatosAcceso.TabIndex = 4;
            this.gbDatosAcceso.TabStop = false;
            this.gbDatosAcceso.Text = "Datos de acceso";
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.SystemColors.Control;
            this.btnGuardar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGuardar.Location = new System.Drawing.Point(146, 155);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(114, 23);
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.Text = "Guardar Usuario";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(126, 25);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(43, 13);
            this.lblUsuario.TabIndex = 7;
            this.lblUsuario.Text = "Usuario";
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(126, 64);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(61, 13);
            this.lblPass.TabIndex = 6;
            this.lblPass.Text = "Contraseña";
            // 
            // lblAlias
            // 
            this.lblAlias.AutoSize = true;
            this.lblAlias.Location = new System.Drawing.Point(126, 103);
            this.lblAlias.Name = "lblAlias";
            this.lblAlias.Size = new System.Drawing.Size(45, 13);
            this.lblAlias.TabIndex = 5;
            this.lblAlias.Text = "Tu Alias";
            // 
            // lblSelfie
            // 
            this.lblSelfie.AutoSize = true;
            this.lblSelfie.Location = new System.Drawing.Point(6, 25);
            this.lblSelfie.Name = "lblSelfie";
            this.lblSelfie.Size = new System.Drawing.Size(42, 13);
            this.lblSelfie.TabIndex = 4;
            this.lblSelfie.Text = "Imagen";
            // 
            // txtAlias
            // 
            this.txtAlias.Location = new System.Drawing.Point(126, 119);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Size = new System.Drawing.Size(134, 20);
            this.txtAlias.TabIndex = 2;
            // 
            // pbSelfie
            // 
            this.pbSelfie.Location = new System.Drawing.Point(6, 41);
            this.pbSelfie.Name = "pbSelfie";
            this.pbSelfie.Size = new System.Drawing.Size(114, 108);
            this.pbSelfie.TabIndex = 2;
            this.pbSelfie.TabStop = false;
            // 
            // pbImagen
            // 
            this.pbImagen.Location = new System.Drawing.Point(12, 12);
            this.pbImagen.Name = "pbImagen";
            this.pbImagen.Size = new System.Drawing.Size(309, 249);
            this.pbImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImagen.TabIndex = 5;
            this.pbImagen.TabStop = false;
            // 
            // frmRegistro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(610, 273);
            this.Controls.Add(this.pbImagen);
            this.Controls.Add(this.gbDatosAcceso);
            this.Controls.Add(this.btnCancelar);
            this.Name = "frmRegistro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de Usuarios";
            this.Load += new System.EventHandler(this.frmRegistro_Load);
            this.gbDatosAcceso.ResumeLayout(false);
            this.gbDatosAcceso.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelfie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnTakeSelfie;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox gbDatosAcceso;
        private System.Windows.Forms.PictureBox pbImagen;
        private System.Windows.Forms.PictureBox pbSelfie;
        private System.Windows.Forms.Label lblSelfie;
        private System.Windows.Forms.TextBox txtAlias;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Label lblAlias;
        private System.Windows.Forms.Button btnGuardar;
    }
}