using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.Util;

namespace SelfieLogin
{
    public partial class frmRegistro : Form
    {
        /*Definicion de Variables*/
        ComputerVision cv;
        private Capture cap;
        Image<Bgr, byte> actualFrame;
        Image<Gray, Byte> trainedFrame;
        List<Image<Gray, Byte>> ListTrainedImages;
        string cadena;
        public int lastID;
        
        public frmRegistro()
        {
            InitializeComponent();
        }

        private void frmRegistro_Load(object sender, EventArgs e)
        {
            //Inicia una nueva instancia de la Clase ComputerVision
            cv = new ComputerVision();
            //Inicia una nueva Captura desde la WebCam
            cap = new Capture(0);
            //Lista de Imagenes Entrenadas donde se guardaran dinamicamente la imagenes que el Usuario tome
            //como imagen de entrenamiento cada vez que presione el boton TakeSelfie
            ListTrainedImages = new List<Image<Gray, byte>>();
            //StringConnection de la DB
            
            //cadena= @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\SelfieLoginDataBase.mdf;Integrated Security=True";
            cadena = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Federico\Documents\Visual Studio 2013\Projects\SelfieLogin\SelfieLogin\SelfieLoginDataBase.mdf;Integrated Security=True";
            IniciarCaptura();
        }
        
        private void IniciarCaptura()
        {
            cap.QueryFrame();
            Application.Idle += new EventHandler(FrameGrabber);
        }

        public void FrameGrabber(object sender, EventArgs e)
        {
            try
            {
                actualFrame=cv.CapturarFrame();
                cv.ConvertirFrameAGrises(actualFrame);
                MCvAvgComp[][] _FacesDetected = cv.FacesDetection();
                foreach (MCvAvgComp f in _FacesDetected[0])
                {
                    cv.DibujarRecognitionRectangle(f);
                }
                pbImagen.Image = actualFrame.ToBitmap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTakeSelfie_Click(object sender, EventArgs e)
        {
            try
            {
                trainedFrame = TakeSelfie();
                ListTrainedImages.Add(trainedFrame);
                pbSelfie.Image = trainedFrame.ToBitmap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        public Image<Gray,Byte> TakeSelfie()
        {
            actualFrame = cv.CapturarFrame();
            cv.ConvertirFrameAGrises(actualFrame);
            MCvAvgComp[][] _FacesDetected = cv.FacesDetection();
            foreach (MCvAvgComp f in _FacesDetected[0])
            {
                cv.CrearTrainedFace(f);
                break;
            }
            return (cv.LastTrainingImage());
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Boolean test1=false;
            Boolean test2 = false;
            try
            {
                //Verifico que los campos no esten vacios
                test1 = ValidarCampos();
                //Verifico que la lista de imagenes de entrenamiento no este vacia
                if (ListTrainedImages.Count!=0)
                {
                    test2 = true;
                }
                if (test1==true)
                {
                    if (test2==true)
                    {
                        // Objetos de conexión y comando
                        SqlConnection conn = new SqlConnection(cadena);
                        SqlCommand cmd = new SqlCommand();

                        ActualizarTUsuarios(conn, cmd);
                        ActualizarTTrainedImages(conn, cmd);

                        MessageBox.Show("El Usuario fue agregado correctamente", "Selfie Login V1.0", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No ha registrado ninguna imagen de entrenamiento. Debe registrar al menos una imagen. (Recomendado: 4 imaganes MINIMO)", "SelfieLogin V1.0", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar todos los campos antes de continuar.", "SelfieLogin V1.0", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Boolean ValidarCampos()
        {
            if (txtUsuario.Text!="")
            {
                if (txtPass.Text!="")
                {
                    if (txtAlias.Text!="")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void ActualizarTUsuarios(SqlConnection conn,SqlCommand cmd)
        {
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO USUARIO (Usuario, Pass, Alias) VALUES (@Usuario, @Pass, @Alias) SELECT SCOPE_IDENTITY()";

            // Creando los parámetros necesarios
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar);
            cmd.Parameters.Add("@Pass", SqlDbType.VarChar);
            cmd.Parameters.Add("@Alias", SqlDbType.VarChar);

            // Asignando los valores a los atributos
            cmd.Parameters["@Usuario"].Value = txtUsuario.Text;
            cmd.Parameters["@Pass"].Value = txtPass.Text;
            cmd.Parameters["@Alias"].Value = txtAlias.Text;

            conn.Open();
            lastID = int.Parse(cmd.ExecuteScalar().ToString());
            conn.Close();
        }
        
        public void ActualizarTTrainedImages(SqlConnection conn,SqlCommand cmd)
        {
            Image TrainedImage;
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO IMAGEN (IdUsuario, Imagen) VALUES (@IdUsuario,@Imagen); SELECT IdUsuario, Imagen FROM IMAGEN WHERE (IdUsuario = @IdUsuario)";
            // Creando los parámetros necesarios
            cmd.Parameters.Add("@IdUsuario", SqlDbType.Int);
            cmd.Parameters.Add("@Imagen", SqlDbType.Image);

            // Asignando los valores a los atributos
            cmd.Parameters["@IdUsuario"].Value = lastID;
            
            foreach(Image<Gray,Byte> tf in ListTrainedImages)
            {
                // Stream usado como buffer
                MemoryStream ms = new MemoryStream();
                // Se guarda la imagen en el buffer
                TrainedImage = tf.ToBitmap();
                TrainedImage.Save(ms, ImageFormat.Jpeg);
                // Se extraen los bytes del buffer para asignarlos como valor para el 
                cmd.Parameters["@Imagen"].Value = ms.GetBuffer();
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
         }
    }
}
