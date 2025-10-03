
class GrafoForm : Form
{
    private List<(string, string)> edges = new List<(string, string)>();
    private HashSet<string> nodes = new HashSet<string>();
    private Dictionary<string, Point> posiciones = new Dictionary<string, Point>();
    private Random rand = new Random();

    public GrafoForm(string rutaArchivo)
    {

        this.Width = 600;
        this.Height = 600;

        LeerArchivo(rutaArchivo);
        GenerarPosiciones();
    }

    private void LeerArchivo(string ruta)
    {
        foreach (var linea in File.ReadAllLines(ruta))
        {
            var partes = linea.Split(' ');
            if (partes.Length == 2)
            {
                edges.Add((partes[0], partes[1]));
                nodes.Add(partes[0]);
                nodes.Add(partes[1]);
            }
        }
    }


    private void GenerarPosiciones()
    {
        foreach (var nodo in nodes)
        {
            posiciones[nodo] = new Point(rand.Next(50, 500), rand.Next(50, 500));
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics g = e.Graphics;
        Pen pen = new Pen(Color.Black, 2);

        // Dibujar aristas
        foreach (var (origen, destino) in edges)
        {
            var p1 = posiciones[origen];
            var p2 = posiciones[destino];
            g.DrawLine(pen, p1, p2);
        }

        // Dibujar nodos
        foreach (var nodo in nodes)
        {
            var pos = posiciones[nodo];
            g.FillEllipse(Brushes.LightBlue, pos.X - 20, pos.Y - 20, 40, 40);
            g.DrawEllipse(Pens.Black, pos.X - 20, pos.Y - 20, 40, 40);
            g.DrawString(nodo, new Font("Arial", 10), Brushes.Black, pos.X - 10, pos.Y - 10);
        }
    }
}



class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        string archivo = "grafo1.txt";
        string archivo2 = "grafo2.txt"; // archivo en la misma carpeta

        GrafoForm form1 = new GrafoForm(archivo);
        GrafoForm form2 = new GrafoForm(archivo2);
        form1.Text = "Grafo 1";
        form2.Text = "Grafo 2";
        form2.Show();
        Application.Run(form1);
    }
}

