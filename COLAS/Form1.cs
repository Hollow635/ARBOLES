using Microsoft.VisualBasic;

namespace COLAS {
    public partial class Form1 : Form {
        const string ruta = @"..\..\..\archivos\ventas.txt";
        const string rutaAux = @"..\..\..\archivos\ventasAux.txt";
        Cola cola = new Cola();

        public Form1(){
            InitializeComponent();
            LeerDesdeArchivo(ruta);
            Mostrar();
        }

        private void EscribirEnArchivo(string ruta) {
            if (File.Exists(ruta))
            {
                using (StreamWriter sw = new StreamWriter(ruta, false))
                {
                    Nodo actual = cola.Ver();

                    while (actual != null)
                    {
                        sw.WriteLine($"{actual.codVendedor}, {actual.impVenta}");
                        actual = actual.Siguiente;
                    }
                }
            }
        }

        private void LeerDesdeArchivo(string ruta) {
            if (File.Exists(ruta))
            {
                using (StreamReader sr = new StreamReader(ruta))
                {
                    string linea;

                    while ((linea = sr.ReadLine()) != null)
                    {
                        string[] datos = linea.Split(',');
                        string codigo = datos[0];
                        decimal importe = Convert.ToDecimal(datos[1]);

                        Nodo venta = new Nodo(codigo, importe);
                        cola.Encolar(venta);
                    }
                }
            }
        }

        private void OrdenarVentas(string ruta) {
            //Lista <NAME DE LA CLASE NODO> NAME PARA LA LISTA = new List<NAME DE LA CLASE NODO>();
            List<Nodo> listaVentas = new List<Nodo>();

            //NAME DE LA CLASE NODO   NUEVO NOMBRE PARA EL OBJECTO = new cola.Ver();
            Nodo actual = cola.Ver();

            while (actual != null)
            {
                listaVentas.Add(new Nodo(actual.codVendedor, actual.impVenta));
                actual = actual.Siguiente;
            }

            listaVentas = listaVentas.OrderBy(a => a.codVendedor).ToList();

            using (StreamWriter sw = new StreamWriter(ruta))
            {
                foreach (var ventas in listaVentas)
                {
                    sw.WriteLine($"{ventas.codVendedor}, {ventas.impVenta}");
                }
            }
        }

        private void Mostrar() {
            listBox1.Items.Clear();

            Nodo actual = cola.Ver();

            while (actual != null)
            {
                listBox1.Items.Add($"Codigo: {actual.codVendedor} - Importe: {actual.impVenta}");
                actual = actual.Siguiente;
            }
        }

        private void CorteControl(string ruta) {
            if (File.Exists(ruta)) {
                using (StreamReader sr = new StreamReader(ruta)) {
                    string _S = sr.ReadLine();
                    string[] _D = _S.Split(',');

                    bool agregar1 = false;

                    decimal _SubTotalventas = 0; //Para almacenar la cantidad de ventas por vendedor
                    decimal _TotalGeneral = 0; //Para almacenar el total producido
                    decimal _PromVentas = 0; //Para el promedio de las ventas
                    decimal _ContVentas = 0; //Para contar las ventas ingresadas

                    while (_S != null) {
                        string _Vendedor = _D[0];
                        dataGridView1.Rows.Add(new string[] { _D[0] });

                        while (_S != null && _Vendedor == _D[0]) {
                            decimal importe = Convert.ToDecimal(_D[1]);
                            _SubTotalventas += importe;
                            _TotalGeneral += importe;
                            _ContVentas++;

                            dataGridView1.Rows.Add(new string[] { "", _D[1] });
                            agregar1 = true;

                            _S = sr.ReadLine();
                            if (_S != null)
                            {
                                _D = _S.Split(',');
                            }
                        }
                        //Mostrar el total de ventas de cada vendedor
                        int filaSub = dataGridView1.Rows.Add(new string[] { "", "SubTotal ->", _SubTotalventas.ToString() });
                        dataGridView1.Rows[filaSub].DefaultCellStyle.BackColor = Color.Aqua; //Pinta la fila de amarillo

                        dataGridView1.Rows.Add(1); //Es un espacio

                        _SubTotalventas = 0;
                        agregar1 = false;
                    }
                    _PromVentas = _TotalGeneral / _ContVentas;
                    //Mostrar el total de todas las ventas
                    //CODIGO, IMPORTE, TOTAL POR VENDEDOR, TOTAL GENERAL, PROM DE VENTAS
                    int filaTotal = dataGridView1.Rows.Add(new string[] { "", "", "TOTAL DE VENTAS ->", _TotalGeneral.ToString() });
                    dataGridView1.Rows[filaTotal].DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            string codigo = Interaction.InputBox("INGRESE SU CODIGO: ");
            decimal importe = Convert.ToDecimal(Interaction.InputBox("INGRESE EL MONTO DEL PRODUCTO/S VENDIDO> "));
            Nodo nueva = new Nodo(codigo, importe);

            cola.Encolar(nueva);
            EscribirEnArchivo(ruta);
            Mostrar();
        }

        private void button2_Click(object sender, EventArgs e) {
            Nodo eliminado = cola.Desencolar();
            if (eliminado != null)
                MessageBox.Show($"SE DESENCOLO AL NODO CON ID {eliminado.codVendedor}");
            else
                MessageBox.Show("NO HAY VALORES EN LA COLA");
            EscribirEnArchivo(ruta);
            Mostrar();
        }

        private void button3_Click(object sender, EventArgs e) {
            Nodo proximo = cola.Ver();
            if (proximo != null)
                MessageBox.Show($"SE DESENCOLO AL NODO CON ID {proximo.codVendedor}");
            else
                MessageBox.Show("NO HAY VALORES EN LA COLA");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OrdenarVentas(rutaAux);
            CorteControl(rutaAux);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
