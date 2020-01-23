namespace RetroElite.Forms
{
	// Token: 0x02000076 RID: 118
	public partial class GestionCuentas : global::DarkUI.Forms.DarkForm
	{
		// Token: 0x060004EE RID: 1262 RVA: 0x0001EB90 File Offset: 0x0001CF90
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001EBC8 File Offset: 0x0001CFC8
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.contextMenuStripFormCuentas = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.conectarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cuentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contraseñaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nombreDelPersonajeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagenesFormCuentas = new System.Windows.Forms.ImageList(this.components);
            this.AgregarCuenta = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox_informacion_agregar_cuenta = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_Agregar_Retroceder = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label_Nombre_Cuenta = new System.Windows.Forms.Label();
            this.label_Password = new System.Windows.Forms.Label();
            this.label_Eleccion_Servidor = new System.Windows.Forms.Label();
            this.label_Nombre_Personaje = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_nombre_personaje = new DarkUI.Controls.DarkTextBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox_Servidor = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_Password = new DarkUI.Controls.DarkTextBox();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_Nombre_Cuenta = new DarkUI.Controls.DarkTextBox();
            this.boton_Agregar_Cuenta = new DarkUI.Controls.DarkButton();
            this.ListaCuentas = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox_informacion = new System.Windows.Forms.PictureBox();
            this.label_informacionClickCuentas = new System.Windows.Forms.Label();
            this.listViewCuentas = new System.Windows.Forms.ListView();
            this.ColumnaNombreCuenta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnaNombreServidor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnaNombrePersonaje = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControlPrincipalCuentas = new System.Windows.Forms.TabControl();
            this.contextMenuStripFormCuentas.SuspendLayout();
            this.AgregarCuenta.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_informacion_agregar_cuenta)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.ListaCuentas.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_informacion)).BeginInit();
            this.tabControlPrincipalCuentas.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripFormCuentas
            // 
            this.contextMenuStripFormCuentas.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripFormCuentas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.conectarToolStripMenuItem,
            this.modificarToolStripMenuItem,
            this.eliminarToolStripMenuItem});
            this.contextMenuStripFormCuentas.Name = "contextMenuStripFormCuentas";
            this.contextMenuStripFormCuentas.Size = new System.Drawing.Size(169, 100);
            // 
            // conectarToolStripMenuItem
            // 
            this.conectarToolStripMenuItem.Name = "conectarToolStripMenuItem";
            this.conectarToolStripMenuItem.Size = new System.Drawing.Size(168, 32);
            this.conectarToolStripMenuItem.Text = "Connexion";
            this.conectarToolStripMenuItem.Click += new System.EventHandler(this.conectarToolStripMenuItem_Click);
            // 
            // modificarToolStripMenuItem
            // 
            this.modificarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cuentaToolStripMenuItem,
            this.contraseñaToolStripMenuItem,
            this.nombreDelPersonajeToolStripMenuItem});
            this.modificarToolStripMenuItem.Name = "modificarToolStripMenuItem";
            this.modificarToolStripMenuItem.Size = new System.Drawing.Size(168, 32);
            this.modificarToolStripMenuItem.Text = "Modifier";
            // 
            // cuentaToolStripMenuItem
            // 
            this.cuentaToolStripMenuItem.Name = "cuentaToolStripMenuItem";
            this.cuentaToolStripMenuItem.Size = new System.Drawing.Size(222, 34);
            this.cuentaToolStripMenuItem.Text = "Compte";
            this.cuentaToolStripMenuItem.Click += new System.EventHandler(this.modificar_Cuenta);
            // 
            // contraseñaToolStripMenuItem
            // 
            this.contraseñaToolStripMenuItem.Name = "contraseñaToolStripMenuItem";
            this.contraseñaToolStripMenuItem.Size = new System.Drawing.Size(222, 34);
            this.contraseñaToolStripMenuItem.Text = "Mot de passe";
            this.contraseñaToolStripMenuItem.Click += new System.EventHandler(this.modificar_Cuenta);
            // 
            // nombreDelPersonajeToolStripMenuItem
            // 
            this.nombreDelPersonajeToolStripMenuItem.Name = "nombreDelPersonajeToolStripMenuItem";
            this.nombreDelPersonajeToolStripMenuItem.Size = new System.Drawing.Size(222, 34);
            this.nombreDelPersonajeToolStripMenuItem.Text = "Personnage";
            this.nombreDelPersonajeToolStripMenuItem.Click += new System.EventHandler(this.modificar_Cuenta);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(168, 32);
            this.eliminarToolStripMenuItem.Text = "Enlever";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // imagenesFormCuentas
            // 
            this.imagenesFormCuentas.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imagenesFormCuentas.ImageSize = new System.Drawing.Size(16, 16);
            this.imagenesFormCuentas.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // AgregarCuenta
            // 
            this.AgregarCuenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.AgregarCuenta.Controls.Add(this.tableLayoutPanel3);
            this.AgregarCuenta.ImageKey = "agregar_cuenta.png";
            this.AgregarCuenta.Location = new System.Drawing.Point(4, 32);
            this.AgregarCuenta.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AgregarCuenta.Name = "AgregarCuenta";
            this.AgregarCuenta.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AgregarCuenta.Size = new System.Drawing.Size(854, 612);
            this.AgregarCuenta.TabIndex = 1;
            this.AgregarCuenta.Text = "Ajouter un compte";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.checkBox_Agregar_Retroceder, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.71429F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.28571F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.714286F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(848, 604);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.374384F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 91.62562F));
            this.tableLayoutPanel4.Controls.Add(this.pictureBox_informacion_agregar_cuenta, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(842, 64);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // pictureBox_informacion_agregar_cuenta
            // 
            this.pictureBox_informacion_agregar_cuenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_informacion_agregar_cuenta.Location = new System.Drawing.Point(3, 3);
            this.pictureBox_informacion_agregar_cuenta.Name = "pictureBox_informacion_agregar_cuenta";
            this.pictureBox_informacion_agregar_cuenta.Size = new System.Drawing.Size(64, 58);
            this.pictureBox_informacion_agregar_cuenta.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_informacion_agregar_cuenta.TabIndex = 1;
            this.pictureBox_informacion_agregar_cuenta.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.75F);
            this.label1.ForeColor = System.Drawing.Color.Coral;
            this.label1.Location = new System.Drawing.Point(73, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(766, 64);
            this.label1.TabIndex = 1;
            this.label1.Text = "Laissez le champ \'\'Personnage\" vide si vous voulez que le bot connecte le premier" +
    " personnage du compte";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // checkBox_Agregar_Retroceder
            // 
            this.checkBox_Agregar_Retroceder.AutoSize = true;
            this.checkBox_Agregar_Retroceder.Checked = true;
            this.checkBox_Agregar_Retroceder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Agregar_Retroceder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox_Agregar_Retroceder.ForeColor = System.Drawing.Color.Coral;
            this.checkBox_Agregar_Retroceder.Location = new System.Drawing.Point(3, 559);
            this.checkBox_Agregar_Retroceder.Name = "checkBox_Agregar_Retroceder";
            this.checkBox_Agregar_Retroceder.Size = new System.Drawing.Size(842, 42);
            this.checkBox_Agregar_Retroceder.TabIndex = 51;
            this.checkBox_Agregar_Retroceder.Text = "Revenez à l\'onglet \'Liste de comptes\' après avoir ajouté le compte.";
            this.checkBox_Agregar_Retroceder.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.boton_Agregar_Cuenta, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 73);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.63636F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.36364F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(842, 480);
            this.tableLayoutPanel5.TabIndex = 5;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.96552F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.03448F));
            this.tableLayoutPanel6.Controls.Add(this.label_Nombre_Cuenta, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.label_Password, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.label_Eleccion_Servidor, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.label_Nombre_Personaje, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 1, 3);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel8, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel9, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel10, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 4;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(836, 414);
            this.tableLayoutPanel6.TabIndex = 2;
            // 
            // label_Nombre_Cuenta
            // 
            this.label_Nombre_Cuenta.AutoSize = true;
            this.label_Nombre_Cuenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Nombre_Cuenta.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Nombre_Cuenta.ForeColor = System.Drawing.Color.Gainsboro;
            this.label_Nombre_Cuenta.Location = new System.Drawing.Point(3, 0);
            this.label_Nombre_Cuenta.Name = "label_Nombre_Cuenta";
            this.label_Nombre_Cuenta.Size = new System.Drawing.Size(236, 103);
            this.label_Nombre_Cuenta.TabIndex = 1;
            this.label_Nombre_Cuenta.Text = "Compte ";
            this.label_Nombre_Cuenta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Password
            // 
            this.label_Password.AutoSize = true;
            this.label_Password.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Password.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label_Password.ForeColor = System.Drawing.Color.Gainsboro;
            this.label_Password.Location = new System.Drawing.Point(3, 103);
            this.label_Password.Name = "label_Password";
            this.label_Password.Size = new System.Drawing.Size(236, 103);
            this.label_Password.TabIndex = 3;
            this.label_Password.Text = "Mot de passe ";
            this.label_Password.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Eleccion_Servidor
            // 
            this.label_Eleccion_Servidor.AutoSize = true;
            this.label_Eleccion_Servidor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Eleccion_Servidor.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label_Eleccion_Servidor.ForeColor = System.Drawing.Color.Gainsboro;
            this.label_Eleccion_Servidor.Location = new System.Drawing.Point(3, 206);
            this.label_Eleccion_Servidor.Name = "label_Eleccion_Servidor";
            this.label_Eleccion_Servidor.Size = new System.Drawing.Size(236, 103);
            this.label_Eleccion_Servidor.TabIndex = 5;
            this.label_Eleccion_Servidor.Text = "Serveur ";
            this.label_Eleccion_Servidor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Nombre_Personaje
            // 
            this.label_Nombre_Personaje.AutoSize = true;
            this.label_Nombre_Personaje.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Nombre_Personaje.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.label_Nombre_Personaje.ForeColor = System.Drawing.Color.Gainsboro;
            this.label_Nombre_Personaje.Location = new System.Drawing.Point(3, 309);
            this.label_Nombre_Personaje.Name = "label_Nombre_Personaje";
            this.label_Nombre_Personaje.Size = new System.Drawing.Size(236, 105);
            this.label_Nombre_Personaje.TabIndex = 7;
            this.label_Nombre_Personaje.Text = "Personnage à connecter";
            this.label_Nombre_Personaje.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.textBox_nombre_personaje, 0, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(245, 312);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 3;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(588, 99);
            this.tableLayoutPanel7.TabIndex = 4;
            // 
            // textBox_nombre_personaje
            // 
            this.textBox_nombre_personaje.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.textBox_nombre_personaje.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_nombre_personaje.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_nombre_personaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.textBox_nombre_personaje.Location = new System.Drawing.Point(3, 36);
            this.textBox_nombre_personaje.MaxLength = 25;
            this.textBox_nombre_personaje.Name = "textBox_nombre_personaje";
            this.textBox_nombre_personaje.Size = new System.Drawing.Size(582, 33);
            this.textBox_nombre_personaje.TabIndex = 5;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.comboBox_Servidor, 0, 1);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(245, 209);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 3;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.39F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.61F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(588, 97);
            this.tableLayoutPanel8.TabIndex = 3;
            // 
            // comboBox_Servidor
            // 
            this.comboBox_Servidor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.comboBox_Servidor.CausesValidation = false;
            this.comboBox_Servidor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_Servidor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Servidor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_Servidor.ForeColor = System.Drawing.Color.Gainsboro;
            this.comboBox_Servidor.FormattingEnabled = true;
            this.comboBox_Servidor.Items.AddRange(new object[] {
            "Algathe",
            "Arty",
            "Ayuto",
            "Bilby",
            "Clustus",
            "Droupik",
            "Eratz",
            "Henual",
            "Hogmeiser",
            "Issering",
            "Nabur"});
            this.comboBox_Servidor.Location = new System.Drawing.Point(3, 34);
            this.comboBox_Servidor.Name = "comboBox_Servidor";
            this.comboBox_Servidor.Size = new System.Drawing.Size(582, 36);
            this.comboBox_Servidor.Sorted = true;
            this.comboBox_Servidor.TabIndex = 6;
            this.comboBox_Servidor.TabStop = false;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.textBox_Password, 0, 1);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(245, 106);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 3;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(588, 97);
            this.tableLayoutPanel9.TabIndex = 2;
            // 
            // textBox_Password
            // 
            this.textBox_Password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.textBox_Password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Password.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.textBox_Password.Location = new System.Drawing.Point(3, 35);
            this.textBox_Password.MaxLength = 25;
            this.textBox_Password.Name = "textBox_Password";
            this.textBox_Password.Size = new System.Drawing.Size(582, 33);
            this.textBox_Password.TabIndex = 4;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Controls.Add(this.textBox_Nombre_Cuenta, 0, 1);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(245, 3);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 3;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(588, 97);
            this.tableLayoutPanel10.TabIndex = 1;
            // 
            // textBox_Nombre_Cuenta
            // 
            this.textBox_Nombre_Cuenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.textBox_Nombre_Cuenta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Nombre_Cuenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Nombre_Cuenta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.textBox_Nombre_Cuenta.Location = new System.Drawing.Point(3, 35);
            this.textBox_Nombre_Cuenta.MaxLength = 25;
            this.textBox_Nombre_Cuenta.Name = "textBox_Nombre_Cuenta";
            this.textBox_Nombre_Cuenta.Size = new System.Drawing.Size(582, 33);
            this.textBox_Nombre_Cuenta.TabIndex = 2;
            // 
            // boton_Agregar_Cuenta
            // 
            this.boton_Agregar_Cuenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boton_Agregar_Cuenta.Location = new System.Drawing.Point(3, 423);
            this.boton_Agregar_Cuenta.Name = "boton_Agregar_Cuenta";
            this.boton_Agregar_Cuenta.Padding = new System.Windows.Forms.Padding(5);
            this.boton_Agregar_Cuenta.Size = new System.Drawing.Size(836, 54);
            this.boton_Agregar_Cuenta.TabIndex = 9;
            this.boton_Agregar_Cuenta.Text = "Ajouter un compte";
            this.boton_Agregar_Cuenta.Click += new System.EventHandler(this.boton_Agregar_Cuenta_Click);
            // 
            // ListaCuentas
            // 
            this.ListaCuentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.ListaCuentas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ListaCuentas.Controls.Add(this.tableLayoutPanel1);
            this.ListaCuentas.ForeColor = System.Drawing.Color.Gainsboro;
            this.ListaCuentas.ImageKey = "lista_cuentas.png";
            this.ListaCuentas.Location = new System.Drawing.Point(4, 32);
            this.ListaCuentas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ListaCuentas.Name = "ListaCuentas";
            this.ListaCuentas.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ListaCuentas.Size = new System.Drawing.Size(854, 612);
            this.ListaCuentas.TabIndex = 0;
            this.ListaCuentas.Text = "Liste des comptes";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listViewCuentas, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.43697F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.563025F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(848, 604);
            this.tableLayoutPanel1.TabIndex = 1;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.15801F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.84199F));
            this.tableLayoutPanel2.Controls.Add(this.pictureBox_informacion, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label_informacionClickCuentas, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 553);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(842, 39);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // pictureBox_informacion
            // 
            this.pictureBox_informacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_informacion.Location = new System.Drawing.Point(3, 3);
            this.pictureBox_informacion.Name = "pictureBox_informacion";
            this.pictureBox_informacion.Size = new System.Drawing.Size(79, 33);
            this.pictureBox_informacion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_informacion.TabIndex = 0;
            this.pictureBox_informacion.TabStop = false;
            this.pictureBox_informacion.Click += new System.EventHandler(this.pictureBox_informacion_Click);
            // 
            // label_informacionClickCuentas
            // 
            this.label_informacionClickCuentas.AutoSize = true;
            this.label_informacionClickCuentas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_informacionClickCuentas.Location = new System.Drawing.Point(88, 0);
            this.label_informacionClickCuentas.Name = "label_informacionClickCuentas";
            this.label_informacionClickCuentas.Size = new System.Drawing.Size(751, 39);
            this.label_informacionClickCuentas.TabIndex = 1;
            this.label_informacionClickCuentas.Text = "Faites un clic droit pour vous connecter/modifier/supprimer un compte \r\nCliquez d" +
    "eux fois sur un compte pour vous connecter";
            this.label_informacionClickCuentas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listViewCuentas
            // 
            this.listViewCuentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.listViewCuentas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnaNombreCuenta,
            this.ColumnaNombreServidor,
            this.ColumnaNombrePersonaje});
            this.listViewCuentas.ContextMenuStrip = this.contextMenuStripFormCuentas;
            this.listViewCuentas.ForeColor = System.Drawing.Color.Gainsboro;
            this.listViewCuentas.FullRowSelect = true;
            this.listViewCuentas.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewCuentas.HideSelection = false;
            this.listViewCuentas.Location = new System.Drawing.Point(3, 4);
            this.listViewCuentas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listViewCuentas.Name = "listViewCuentas";
            this.listViewCuentas.Size = new System.Drawing.Size(840, 542);
            this.listViewCuentas.TabIndex = 1;
            this.listViewCuentas.UseCompatibleStateImageBehavior = false;
            this.listViewCuentas.View = System.Windows.Forms.View.Details;
            this.listViewCuentas.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listViewCuentas_ColumnWidthChanging);
            this.listViewCuentas.SelectedIndexChanged += new System.EventHandler(this.listViewCuentas_SelectedIndexChanged);
            this.listViewCuentas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewCuentas_MouseDoubleClick);
            // 
            // ColumnaNombreCuenta
            // 
            this.ColumnaNombreCuenta.Text = "Nom de compte";
            this.ColumnaNombreCuenta.Width = 148;
            // 
            // ColumnaNombreServidor
            // 
            this.ColumnaNombreServidor.Text = "Serveur";
            this.ColumnaNombreServidor.Width = 107;
            // 
            // ColumnaNombrePersonaje
            // 
            this.ColumnaNombrePersonaje.Text = "Personnage";
            this.ColumnaNombrePersonaje.Width = 184;
            // 
            // tabControlPrincipalCuentas
            // 
            this.tabControlPrincipalCuentas.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tabControlPrincipalCuentas.Controls.Add(this.ListaCuentas);
            this.tabControlPrincipalCuentas.Controls.Add(this.AgregarCuenta);
            this.tabControlPrincipalCuentas.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControlPrincipalCuentas.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.tabControlPrincipalCuentas.ImageList = this.imagenesFormCuentas;
            this.tabControlPrincipalCuentas.ItemSize = new System.Drawing.Size(137, 28);
            this.tabControlPrincipalCuentas.Location = new System.Drawing.Point(1, 0);
            this.tabControlPrincipalCuentas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControlPrincipalCuentas.Name = "tabControlPrincipalCuentas";
            this.tabControlPrincipalCuentas.SelectedIndex = 0;
            this.tabControlPrincipalCuentas.Size = new System.Drawing.Size(862, 648);
            this.tabControlPrincipalCuentas.TabIndex = 0;
            // 
            // GestionCuentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 631);
            this.Controls.Add(this.tabControlPrincipalCuentas);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(879, 687);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(479, 437);
            this.Name = "GestionCuentas";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion de compte";
            this.Load += new System.EventHandler(this.GestionCuentas_Load);
            this.contextMenuStripFormCuentas.ResumeLayout(false);
            this.AgregarCuenta.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_informacion_agregar_cuenta)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.ListaCuentas.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_informacion)).EndInit();
            this.tabControlPrincipalCuentas.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		// Token: 0x040002F8 RID: 760
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040002F9 RID: 761
		private global::System.Windows.Forms.ImageList imagenesFormCuentas;

		// Token: 0x040002FA RID: 762
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStripFormCuentas;

		// Token: 0x040002FB RID: 763
		private global::System.Windows.Forms.ToolStripMenuItem conectarToolStripMenuItem;

		// Token: 0x040002FC RID: 764
		private global::System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;

		// Token: 0x040002FD RID: 765
		private global::System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;

		// Token: 0x040002FE RID: 766
		private global::System.Windows.Forms.ToolStripMenuItem cuentaToolStripMenuItem;

		// Token: 0x040002FF RID: 767
		private global::System.Windows.Forms.ToolStripMenuItem contraseñaToolStripMenuItem;

		// Token: 0x04000300 RID: 768
		private global::System.Windows.Forms.ToolStripMenuItem nombreDelPersonajeToolStripMenuItem;

		// Token: 0x04000301 RID: 769
		private global::System.Windows.Forms.TabPage AgregarCuenta;

		// Token: 0x04000302 RID: 770
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;

		// Token: 0x04000303 RID: 771
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;

		// Token: 0x04000304 RID: 772
		private global::System.Windows.Forms.PictureBox pictureBox_informacion_agregar_cuenta;

		// Token: 0x04000305 RID: 773
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000306 RID: 774
		private global::System.Windows.Forms.CheckBox checkBox_Agregar_Retroceder;

		// Token: 0x04000307 RID: 775
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;

		// Token: 0x04000308 RID: 776
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;

		// Token: 0x04000309 RID: 777
		private global::System.Windows.Forms.Label label_Nombre_Cuenta;

		// Token: 0x0400030A RID: 778
		private global::System.Windows.Forms.Label label_Password;

		// Token: 0x0400030B RID: 779
		private global::System.Windows.Forms.Label label_Eleccion_Servidor;

		// Token: 0x0400030C RID: 780
		private global::System.Windows.Forms.Label label_Nombre_Personaje;

		// Token: 0x0400030D RID: 781
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;

		// Token: 0x0400030E RID: 782
		private global::DarkUI.Controls.DarkTextBox textBox_nombre_personaje;

		// Token: 0x0400030F RID: 783
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;

		// Token: 0x04000310 RID: 784
		private global::System.Windows.Forms.ComboBox comboBox_Servidor;

		// Token: 0x04000311 RID: 785
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;

		// Token: 0x04000312 RID: 786
		private global::DarkUI.Controls.DarkTextBox textBox_Password;

		// Token: 0x04000313 RID: 787
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;

		// Token: 0x04000314 RID: 788
		private global::DarkUI.Controls.DarkTextBox textBox_Nombre_Cuenta;

		// Token: 0x04000315 RID: 789
		private global::DarkUI.Controls.DarkButton boton_Agregar_Cuenta;

		// Token: 0x04000316 RID: 790
		private global::System.Windows.Forms.TabPage ListaCuentas;

		// Token: 0x04000317 RID: 791
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

		// Token: 0x04000318 RID: 792
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;

		// Token: 0x04000319 RID: 793
		private global::System.Windows.Forms.PictureBox pictureBox_informacion;

		// Token: 0x0400031A RID: 794
		private global::System.Windows.Forms.Label label_informacionClickCuentas;

		// Token: 0x0400031B RID: 795
		private global::System.Windows.Forms.ListView listViewCuentas;

		// Token: 0x0400031C RID: 796
		private global::System.Windows.Forms.ColumnHeader ColumnaNombreCuenta;

		// Token: 0x0400031D RID: 797
		private global::System.Windows.Forms.ColumnHeader ColumnaNombreServidor;

		// Token: 0x0400031E RID: 798
		private global::System.Windows.Forms.ColumnHeader ColumnaNombrePersonaje;

		// Token: 0x0400031F RID: 799
		private global::System.Windows.Forms.TabControl tabControlPrincipalCuentas;
	}
}
