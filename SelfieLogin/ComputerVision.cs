using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.Util;

namespace SelfieLogin
{
    public class ComputerVision
    {
        //Atributos
        private Capture _Capture;
        private HaarCascade _HaarCascade;
        private Image<Bgr, Byte> _ActualFrame;
        private Image<Gray, Byte> _GrayFrame,_TrainedFace;
        
        //Metodos
        public ComputerVision()
        {
            _Capture = new Capture(0);
            _HaarCascade = new HaarCascade(Application.StartupPath + @"\HaarCascades\haarcascade_frontalface_default.xml");
        }
        public Image<Bgr,Byte> CapturarFrame()
        {
            //Captura un frame desde la webcam y lo devuelve para su tratamiento
            _ActualFrame = _Capture.QueryFrame().Resize(640, 480, INTER.CV_INTER_CUBIC);
            return _ActualFrame;
        }
        public void ConvertirFrameAGrises(Image<Bgr,Byte> _ActualFrame) 
        {
            //Convierte el frame capturado a escala de grises
            _GrayFrame = _ActualFrame.Convert<Gray, Byte>();
        }
        public MCvAvgComp[][] FacesDetection()
        {
            //algoritmo de voila jones para detectar la cantidad de rostros que aparecen en el frame
            return (_GrayFrame.DetectHaarCascade(_HaarCascade, 1.2, 2, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20)));
        }
        public void CrearTrainedFace(MCvAvgComp face)
        {
            //_TrainedFace se utiliza para enviar al proceso de reconocimiento recognizer.Recognize(_TrainedFace);
            _TrainedFace = _ActualFrame.Copy(face.rect).Convert<Gray, byte>().Resize(100, 100, INTER.CV_INTER_CUBIC);
        }
        public void DibujarRecognitionRectangle(MCvAvgComp face)
        {
            //Dibuja un rectangulo de color verde delimitando el o los rostros detectados
            _ActualFrame.Draw(face.rect, new Bgr(Color.Green), 2);
        }
        public Image<Gray,Byte> LastTrainingImage()
        {
            //Devuelve la ultima imagen de rostro detectado como parte del proceso de entrenamiento
            return _TrainedFace;
        }
        /*private void FrameGrabber(object sender, EventArgs e) 
        {
            try
            {
                CapturarFrame();
                ConvertirFrameAGrises(_ActualFrame);
                MCvAvgComp[][] _FacesDetected = _GrayFrame.DetectHaarCascade(_HaarCascade, 1.2, 2, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));
                foreach (MCvAvgComp f in _FacesDetected[0])
                {
                    CrearTrainedFace(f);
                    DibujarRecognitionRectangle(f);
                }
                //ActualFrame();
                //pbImagen.Image = _ActualFrame.ToBitmap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/
    }   
}
