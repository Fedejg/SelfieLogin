using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.Util;

namespace SelfieLogin
{
    public partial class frmStart : Form
    {
        ComputerVision cv;
        private Capture cap;
        //Lista de imagenes entrenadas tomadas desde la DB que se envian a reconocimiento
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        //Lista de los alias tomados desde la DB que se envian a reconocimiento
        List<string> aliasList = new List<string>();
        //Lista de Objetos Usuario
        List<Usuario> userList = new List<Usuario>();
        //Almacena el frame actual tomado desde la webcam
        Image<Bgr, byte> actualFrame;
        //Almacena el rostro detectado que debe ser reconocido
        Image<Gray, byte> Face;
        //Almacena la imagen obtenida desde la DB que sera guardada en la lista de reconocimiento
        Image<Gray, byte> userImage;
        //Cuenta la cantidad de usuarios que hay regitrados en la DB
        int ContUsuarios = 0;
        //Almacena el Alias del Usuario detectado
        string aliasDetected;
        //Bandera que indica que el usuario fue detectado o no
        bool userDetected = false;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.4d, 0.4d);

        int maxListaTImages = 0, maxListaAlias = 0, maxListaUsuarios = 0;
        
        public frmStart()
        {
            InitializeComponent();
        }

        private void frmStart_Load(object sender, EventArgs e)
        {
            //Se inicia la Aplicacion
            Inicializar();
        }   

        public void Inicializar()
        { 
            //Imagen GIF que representara a la Aplicacion en busqueda con el respectivo mensaje
            pbVision.Image = Image.FromFile(Application.StartupPath + @"\Imagenes\Buscandote.gif");
            lblSaludo.Text = "Buscandote...";
            //Inicializo el objeto de captura que se utilizara desde la webcam
            cap = new Capture(0);
            cv = new ComputerVision();
            //Leo la base de datos para buscar usuarios ya registrados
            ReadDB();
            //Inicializo la captura de la webcam
            IniciarCaptura();
        }

        public void ReadDB()
        {
            //LimpiarListas();
            try
            {
                ActualizarTabla();
                CrearListas();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        private void ActualizarTabla()
        {
            // TODO: esta línea de código carga datos en la tabla 'selfieLoginDataBaseDataSet.Imagen' Puede moverla o quitarla según sea necesario.
            this.imagenTableAdapter.Fill(this.selfieLoginDataBaseDataSet.Imagen);
            // TODO: esta línea de código carga datos en la tabla 'selfieLoginDataBaseDataSet.Usuario' Puede moverla o quitarla según sea necesario.
            this.usuarioTableAdapter.Fill(this.selfieLoginDataBaseDataSet.Usuario);
        }

        private void CrearListas()
        {
            int idActual = 0;
            if (selfieLoginDataBaseDataSet.Usuario.Count != 0)
            {
                foreach (var fila in selfieLoginDataBaseDataSet.Usuario)
                {
                    Usuario us = new Usuario();
                    //Cuento la cantidad de Usuarios registrados en la DB
                    ContUsuarios += 1;
                    //Asigno los parametros desde la DB al Objeto Usuario
                    idActual = fila.IdUsuario;
                    us.User = fila.Usuario;
                    us.Pass = fila.Pass;
                    us.Alias = fila.Alias;
                    foreach (var filaT in selfieLoginDataBaseDataSet.Imagen)
                    {
                        if (idActual == filaT.IdUsuario)
                        {
                            //Proceso la Imagen desde la DB mediante la creacion de un buffer y la transformo a Imagen
                            byte[] imgBuffer = (byte[])filaT["Imagen"];
                            MemoryStream ms = new MemoryStream(imgBuffer);
                            //Cargo la imagen desde la DB a lista de imagenes del Usuario con el Id Actual
                            us.ListTrainedImage.Add(Image.FromStream(ms));

                            //Cargo ademas la imagen en la lista que se enviara al reconocedor
                            //Convierto la Imagen obtenida de la DB a un bitmap
                            Bitmap bmp = (Bitmap)Image.FromStream(ms);
                            //Convierto el bitmap en EmguCV.Image<Gray,Byte>
                            userImage = new Image<Gray, byte>(bmp);
                            trainingImages.Add(userImage);

                            //Cargo ademas el alias en la lista de alias que se enviara al reconocedor
                            aliasList.Add(us.Alias);
                        }
                    }
                    //Asigno el Usuario extraido de la DB a la Lista de Usuarios
                    userList.Add(us);
                }
            }
            else
            {
                //Si la DB esta vacia muestro un mensaje de error
                MessageBox.Show("No existe ningún usuario en al base de datos", "SelfieLogin V1.0", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarListas()
        {
            for (int i = maxListaTImages - 1; i >= 0; i--)
            {
                trainingImages.RemoveAt(i);
            }
            for (int i = maxListaUsuarios - 1; i >= 0; i--)
            {
                userList.RemoveAt(i);
            }
            for (int i = maxListaAlias - 1; i >= 0; i--)
            {
                aliasList.RemoveAt(i);
            }
        }

        private void IniciarCaptura()
        {
            //Inicializo la captura desde la webcam
            cap.QueryFrame();
            Application.Idle += new EventHandler(FrameGrabber);
        }

        private void DetenerCaptura()
        {
            cap.QueryFrame();
            Application.Idle -= new EventHandler(FrameGrabber);
        }

        void FrameGrabber(object sender, EventArgs e)
        {
            try
            {
                actualFrame = cv.CapturarFrame();
                cv.ConvertirFrameAGrises(actualFrame);
                MCvAvgComp[][] _FacesDetected = cv.FacesDetection();
                foreach (MCvAvgComp f in _FacesDetected[0])
                {
                    cv.CrearTrainedFace(f);
                    Face = cv.LastTrainingImage();
                    /*if (trainingImages.ToArray().Length!=0)
	                {
                        MCvTermCriteria termCriterio=new MCvTermCriteria(trainingImages.ToArray().Length,0.001);	 
	                    EigenObjectRecognizer recognizer=new EigenObjectRecognizer(trainingImages.ToArray(),aliasList.ToArray(), 3000, ref termCriterio);
                        aliasDetected=recognizer.Recognize(Face);
                        actualFrame.Draw(aliasDetected,ref font, new Point(f.rect.X-2,f.rect.Y-2),new Bgr(Color.Green));
                    }*/
                }
                //pbImagen.Image = actualFrame.ToBitmap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Capturo la Imagen desde la Webcam para hacer la comparacion con la DB
            try
            {
                actualFrame = cv.CapturarFrame();
                cv.ConvertirFrameAGrises(actualFrame);
                MCvAvgComp[][] _FacesDetected = cv.FacesDetection();
                foreach (MCvAvgComp f in _FacesDetected[0])
                {
                    cv.CrearTrainedFace(f);
                    Face = cv.LastTrainingImage();
                    if (trainingImages.ToArray().Length != 0)
                    {
                        //Defino el criterio con el que se realizara el reconocimiento facial
                        MCvTermCriteria termCriterio = new MCvTermCriteria(trainingImages.ToArray().Length, 0.001);
                        //Defino el Algoritmo EigenFaces y le paso las Imagenes y Alias para realizar el reconocimiento
                        EigenObjectRecognizer recognizer = new EigenObjectRecognizer(trainingImages.ToArray(), aliasList.ToArray(), 3000, ref termCriterio);
                        //Ejecuto EigenFaces y obtengo el Alias del Usuario reconocido
                        aliasDetected = recognizer.Recognize(Face);
                        //actualFrame.Draw(aliasDetected, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.Green));
                        
                        //Recorro la lista de Usuarios para validad que los datos ingresados sean correctos
                        foreach (var us in userList)
                        {
                            if (aliasDetected==us.Alias)
                            {
                                if (txtUsuario.Text==us.User)
                                {
                                    if (txtPass.Text==us.Pass)
                                    {
                                        userDetected = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    //Muestro un mensaje de error y pongo el foco nuevamente en el campo Usuario
                                    MessageBox.Show("El nombre de usuario o la contraseña ingresados no son correctos. Por favor, verifiquelos y vuelva a intentarlo", "Selfie Login V1.0", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtUsuario.Focus();
                                    txtUsuario.SelectAll();
                                    break;
                                }
                            }
                        }
                    }
                }
                if (userDetected == true)
                {
                    pbVision.Image = Image.FromFile(Application.StartupPath + @"\Imagenes\TeEncontre.gif");
                    lblSaludo.Text = "Bienvenido " + aliasDetected + "!!";
                    lblSaludo.Location = new Point(350, 220);
                    DetenerCaptura();
                    pbImagen.Image = null;
                    txtUsuario.Clear();
                    txtPass.Clear();
                    //Linea de la app que se desea gestionar
                    //Process.Start("explorer.exe");
                    //Process.Start(@"C:\Users\Netversity\Documents\Visual Studio 2013\Projects\AlgoritmoGenetico\AlgoritmoGenetico\bin\Debug\AlgoritmoGenetico.exe");
                }
                else 
                {
                    MessageBox.Show("El Usuario no fue reconocido porque no fue registrado. Registre el usuario ingresando a Registrar Usuario", "Selfie Login V1.0", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //pbImagen.Image = nextFrame.ToBitmap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            pbImagen.Image = null;
            DetenerCaptura();
            frmRegistro frm = new frmRegistro();
            frm.ShowDialog();
            IniciarCaptura();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
