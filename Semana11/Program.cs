using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana11
{
    internal class Program
    {
        static int max_estudiantes = 40;
        static int max_cursos = 4;
        static string[] nombreCursos = { "Matematicas", "Fisica", "Programacion", "Base de datos" };
        static string[] estudiante=new string[max_estudiantes];
        static double[,] matrizNotas = new double[max_estudiantes, max_cursos];
        static int total_estudiantes = 0;
        static string rutaArchivo = "notas_curso.txt";
        public static void RegistrarEstudiante()
        {
            Console.WriteLine("REGISTRO DE ESTUDIANTE");
            if (total_estudiantes>=max_estudiantes)
            {
                Console.WriteLine("Se ha alcanzó la capacidad maxima");
                return;
            }
            string nombre=Validador.LeerTexto("Ingrese el nombres y apellidos: ").ToUpper();
            estudiante[total_estudiantes]=nombre;
            Console.WriteLine("Ingrese las notas del estudiante: ");
            for (int i = 0; i < max_cursos; i++)
            {
                matrizNotas[total_estudiantes, i] = Validador.LeerDecimales($"Ingrese la nota de {nombreCursos[i]}: ");
            }
            total_estudiantes++;
            Console.WriteLine("Estudiante registrado correctamente");
        }
        public static void guardarArchivo()
        {
            Console.WriteLine("Guardar registros en archivo");
            if (total_estudiantes == 0)
            {
                Console.WriteLine("No hay registros para guardar");
                return;
            }
            using (StreamWriter escritor =new StreamWriter(rutaArchivo))
            {
                for (int i = 0; i < total_estudiantes; i++)
                {
                    string linea = estudiante[i];
                    for (int j = 0; j < max_cursos; j++)
                    {
                        linea +=","+matrizNotas[i, j];
                    }
                    escritor.WriteLine(linea);
                }
            }
            Console.WriteLine("Registros guardados");
        }
        public static void CargarRegistros()
        {
            Console.WriteLine("CARGAR REGISTROS DESDE ARCHIVO");
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("No existe el archivo de registros");
                return;
            }
            total_estudiantes = 0;
            using(StreamReader lector=new StreamReader(rutaArchivo))
            {
                string linea;
                while ((linea = lector.ReadLine()) != null && total_estudiantes < max_estudiantes)
                {
                    string[] partes = linea.Split(',');
                    if(partes.Length == max_cursos + 1)
                    {
                        estudiante[total_estudiantes] = partes[0];
                        for(int i=0; i < max_cursos; i++)
                        {
                            matrizNotas[total_estudiantes, i] = Convert.ToDouble(partes[i+1]);
                        }
                    }
                    total_estudiantes++;
                }
            }
            Console.WriteLine("Registros cargados correctamente");
        }
        public static void MostrarRegistros()
        {
            Console.WriteLine(new string('=', 100));
            Console.WriteLine("\t\tREGISTRO GENERAL DE NOTAS POR CURSO");
            Console.WriteLine(new string('=', 100));
            if(total_estudiantes == 0)
            {
                Console.WriteLine("No hay registros para mostrar");
                return;
            }
            Console.Write($"{ "Estudiante",-25}|");
            for (int i = 0; i < max_cursos; i++)
            {
                Console.Write($"{nombreCursos[i],-12}|");
            }
            Console.WriteLine("PROMEDIO");
            Console.WriteLine(new string('-', 100));
            for (int i = 0; i < total_estudiantes; i++)
            {
                Console.Write($"{estudiante[i],-25}|");
                double suma = 0;
                for (int j = 0; j < max_cursos; j++)
                {
                    Console.Write($"{matrizNotas[i, j],-12:F1}|");
                    suma += matrizNotas[i, j];
                }
                double promedio = suma / max_cursos;
                string estado=promedio>=12?"Aprobado":"Reprobado";
                Console.WriteLine($"{promedio,-12:F2}({estado})");
                //F2 significa que el promedio se mostrará con 2 decimales
                //si quieres con 3 decimales, puedes usar F3, y así sucesivamente

            }
        }
        static void Main(string[] args)
        {
            int opc=0;
            while (opc != 5)
            {
                Console.Clear();
                Console.WriteLine("[1] Registrar estudiante");
                Console.WriteLine("[2] Guardar en archivo");
                Console.WriteLine("[3] Cargar registros desde archivo");
                Console.WriteLine("[4] Mostrar registros");
                Console.WriteLine("[5] Salir");
                opc = Validador.LeerEntero("Seleccionar opciom[1-5]: ");
                switch(opc)
                {
                    case 1:
                        RegistrarEstudiante();
                        break;
                    case 2:
                        guardarArchivo();
                        break;
                    case 3:
                        CargarRegistros();
                        break;
                    case 4:
                        MostrarRegistros();
                        break;
                    case 5:
                        Console.WriteLine("MUCHOS EXITOS PARA EL NUEVO SEMESTRE...");
                        break;
                }
                Console.ReadKey();

            }
        }
    }
}
