namespace RetroElite.Forms
{
	// Token: 0x02000077 RID: 119
	public partial class Opciones : global::DarkUI.Forms.DarkForm
	{
		// Token: 0x060004F8 RID: 1272 RVA: 0x00020EAC File Offset: 0x0001F2AC
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00020EE4 File Offset: 0x0001F2E4
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::RetroElite.Forms.Opciones));
			this.tableLayoutPanel1 = new global::System.Windows.Forms.TableLayoutPanel();
			this.groupBox1 = new global::System.Windows.Forms.GroupBox();
			this.tableLayoutPanel2 = new global::System.Windows.Forms.TableLayoutPanel();
			this.checkBox_mensajes_debug = new global::System.Windows.Forms.CheckBox();
			this.groupBox2 = new global::System.Windows.Forms.GroupBox();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.boton_opciones_guardar = new global::DarkUI.Controls.DarkButton();
			this.tableLayoutPanel3 = new global::System.Windows.Forms.TableLayoutPanel();
			this.textBox_ip_servidor = new global::DarkUI.Controls.DarkTextBox();
			this.label_ip_conexion = new global::System.Windows.Forms.Label();
			this.label_puerto_servidor = new global::System.Windows.Forms.Label();
			this.textBox_puerto_servidor = new global::DarkUI.Controls.DarkTextBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
			this.tableLayoutPanel1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new global::System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 20.43011f));
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 79.56989f));
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableLayoutPanel1.Size = new global::System.Drawing.Size(437, 372);
			this.tableLayoutPanel1.TabIndex = 0;
			this.groupBox1.Controls.Add(this.tableLayoutPanel2);
			this.groupBox1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.ForeColor = global::System.Drawing.Color.CornflowerBlue;
			this.groupBox1.Location = new global::System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new global::System.Drawing.Size(431, 70);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "General";
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.checkBox_mensajes_debug, 0, 0);
			this.tableLayoutPanel2.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Font = new global::System.Drawing.Font("Segoe UI", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.tableLayoutPanel2.Location = new global::System.Drawing.Point(3, 21);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableLayoutPanel2.Size = new global::System.Drawing.Size(425, 46);
			this.tableLayoutPanel2.TabIndex = 0;
			this.checkBox_mensajes_debug.AutoSize = true;
			this.checkBox_mensajes_debug.Checked = true;
			this.checkBox_mensajes_debug.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.checkBox_mensajes_debug.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.checkBox_mensajes_debug.ForeColor = global::System.Drawing.Color.FloralWhite;
			this.checkBox_mensajes_debug.Location = new global::System.Drawing.Point(3, 3);
			this.checkBox_mensajes_debug.Name = "checkBox_mensajes_debug";
			this.checkBox_mensajes_debug.Size = new global::System.Drawing.Size(419, 40);
			this.checkBox_mensajes_debug.TabIndex = 0;
			this.checkBox_mensajes_debug.Text = "Mode debug";
			this.checkBox_mensajes_debug.UseVisualStyleBackColor = true;
			this.checkBox_mensajes_debug.CheckedChanged += new global::System.EventHandler(this.checkBox_mensajes_debug_CheckedChanged);
			this.groupBox2.Controls.Add(this.panel1);
			this.groupBox2.Controls.Add(this.tableLayoutPanel3);
			this.groupBox2.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.ForeColor = global::System.Drawing.Color.CornflowerBlue;
			this.groupBox2.Location = new global::System.Drawing.Point(3, 79);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new global::System.Drawing.Size(431, 290);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Serveur de jeu ";
			this.panel1.Controls.Add(this.boton_opciones_guardar);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new global::System.Drawing.Point(3, 239);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(425, 48);
			this.panel1.TabIndex = 1;
			this.boton_opciones_guardar.AllowDrop = true;
			this.boton_opciones_guardar.AutoSize = true;
			this.boton_opciones_guardar.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.boton_opciones_guardar.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.boton_opciones_guardar.Font = new global::System.Drawing.Font("Segoe UI", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.boton_opciones_guardar.Location = new global::System.Drawing.Point(0, 0);
			this.boton_opciones_guardar.Name = "boton_opciones_guardar";
			this.boton_opciones_guardar.Padding = new global::System.Windows.Forms.Padding(5);
			this.boton_opciones_guardar.Size = new global::System.Drawing.Size(425, 48);
			this.boton_opciones_guardar.TabIndex = 0;
			this.boton_opciones_guardar.Text = "Sauvegarder";
			this.boton_opciones_guardar.Click += new global::System.EventHandler(this.boton_opciones_guardar_Click);
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 24f));
			this.tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 76f));
			this.tableLayoutPanel3.Controls.Add(this.label_ip_conexion, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.textBox_ip_servidor, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.label_puerto_servidor, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.textBox_puerto_servidor, 1, 1);
			this.tableLayoutPanel3.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel3.Location = new global::System.Drawing.Point(3, 21);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 59.33014f));
			this.tableLayoutPanel3.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 40.66986f));
			this.tableLayoutPanel3.Size = new global::System.Drawing.Size(425, 218);
			this.tableLayoutPanel3.TabIndex = 0;
			this.textBox_ip_servidor.Anchor = global::System.Windows.Forms.AnchorStyles.Left;
			this.textBox_ip_servidor.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox_ip_servidor.ForeColor = global::System.Drawing.Color.FromArgb(220, 220, 220);
			this.textBox_ip_servidor.Location = new global::System.Drawing.Point(105, 52);
			this.textBox_ip_servidor.MaxLength = 35;
			this.textBox_ip_servidor.Name = "textBox_ip_servidor";
			this.textBox_ip_servidor.Size = new global::System.Drawing.Size(317, 25);
			this.textBox_ip_servidor.TabIndex = 1;
			this.label_ip_conexion.Anchor = global::System.Windows.Forms.AnchorStyles.Left;
			this.label_ip_conexion.AutoSize = true;
			this.label_ip_conexion.Location = new global::System.Drawing.Point(3, 56);
			this.label_ip_conexion.Name = "label_ip_conexion";
			this.label_ip_conexion.Size = new global::System.Drawing.Size(84, 17);
			this.label_ip_conexion.TabIndex = 0;
			this.label_ip_conexion.Text = "IP du serveur";
			this.label_ip_conexion.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.label_ip_conexion.Click += new global::System.EventHandler(this.label_ip_conexion_Click);
			this.label_puerto_servidor.AutoSize = true;
			this.label_puerto_servidor.ImageAlign = global::System.Drawing.ContentAlignment.TopLeft;
			this.label_puerto_servidor.Location = new global::System.Drawing.Point(3, 129);
			this.label_puerto_servidor.Name = "label_puerto_servidor";
			this.label_puerto_servidor.Size = new global::System.Drawing.Size(33, 22);
			this.label_puerto_servidor.TabIndex = 2;
			this.label_puerto_servidor.Text = "Port ";
			this.label_puerto_servidor.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.label_puerto_servidor.UseCompatibleTextRendering = true;
			this.label_puerto_servidor.Click += new global::System.EventHandler(this.label_puerto_servidor_Click);
			this.textBox_puerto_servidor.Location = new global::System.Drawing.Point(105, 132);
			this.textBox_puerto_servidor.MaxLength = 5;
			this.textBox_puerto_servidor.Name = "textBox_puerto_servidor";
			this.textBox_puerto_servidor.Size = new global::System.Drawing.Size(317, 25);
			this.textBox_puerto_servidor.TabIndex = 3;
			this.textBox_puerto_servidor.TextChanged += new global::System.EventHandler(this.textBox_puerto_servidor_TextChanged);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 17f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			base.ClientSize = new global::System.Drawing.Size(437, 372);
			base.Controls.Add(this.tableLayoutPanel1);
			this.Font = new global::System.Drawing.Font("Segoe UI", 9.75f);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			base.MaximizeBox = false;
			this.MaximumSize = new global::System.Drawing.Size(500, 411);
			base.MinimizeBox = false;
			this.MinimumSize = new global::System.Drawing.Size(400, 311);
			base.Name = "Opciones";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Options";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000320 RID: 800
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000321 RID: 801
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

		// Token: 0x04000322 RID: 802
		private global::System.Windows.Forms.GroupBox groupBox1;

		// Token: 0x04000323 RID: 803
		private global::System.Windows.Forms.GroupBox groupBox2;

		// Token: 0x04000324 RID: 804
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;

		// Token: 0x04000325 RID: 805
		private global::System.Windows.Forms.CheckBox checkBox_mensajes_debug;

		// Token: 0x04000326 RID: 806
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000327 RID: 807
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;

		// Token: 0x04000328 RID: 808
		private global::System.Windows.Forms.Label label_ip_conexion;

		// Token: 0x04000329 RID: 809
		private global::System.Windows.Forms.Label label_puerto_servidor;

		// Token: 0x0400032A RID: 810
		private global::System.Windows.Forms.TextBox textBox_puerto_servidor;

		// Token: 0x0400032B RID: 811
		private global::DarkUI.Controls.DarkButton boton_opciones_guardar;

		// Token: 0x0400032C RID: 812
		private global::DarkUI.Controls.DarkTextBox textBox_ip_servidor;
	}
}
