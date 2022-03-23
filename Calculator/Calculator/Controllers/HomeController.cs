
using Calculator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Calculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet] //opcional -> se não se especificar, escolhe sempre o Get
        public IActionResult Index()
        {
            //preparar os dados para serem enviados para a View na primeira interação
            ViewBag.Visor = "0";

            return View();
        }

        [HttpPost] //Quando o formulário for submetido em 'post', ele será acionado
        public IActionResult Index(string botao, string visor, string primeiroOperando, string operador, double result, string LimpaEcra)
        {


            //testar valor do 'botao'

            switch (botao)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                    //pressionou um algarismo
                    //Sugestão -> Fazer de forma algébrica
                    if (visor == "0" || LimpaEcra == "sim")
                    {
                        visor = botao;
                    }
                    else
                    {
                        LimpaEcra = "nao";
                        visor += botao;
                    }
                    break;

                case ",":
                    if (!visor.Contains(','))
                    {
                        visor += botao;
                    }
                    break;

                case "+/-":
                    //vai inverter o valor do visor 
                    //Sugestão -> Fazer de forma algébrica
                    if (!visor.StartsWith("-")) // ou então !visor.Contains("-")
                    {
                        visor = "-" + visor;
                    }
                    else
                    {
                        visor = visor[1..]; //Semelhante a visor.SubString(1) 
                                            //ou então visor.Remove(0, 1);-> Remove do indice 0 ao indice 1 (exclusive)
                    }
                    break;

                case "C":
                    visor = "0";
                    primeiroOperando = "0";
                    operador = "0";
                    break;

                case "+":
                    if(operador != "0") {
                        switch (operador)
                        {
                            case "+":

                                result = Convert.ToDouble(visor) + Convert.ToDouble(primeiroOperando);
                                

                                break;
                            case "-":
                                result =Convert.ToDouble(visor) - Convert.ToDouble(primeiroOperando);
                                
                                break;

                            case "*":
                                result = Convert.ToDouble(visor) * Convert.ToDouble(primeiroOperando);
                                
                                break;
                            case ":":
                                result = Convert.ToDouble(visor) / Convert.ToDouble(primeiroOperando);
                               
                                break;
                                visor = Convert.ToString(result);
                                primeiroOperando = visor;


                        }
                    
                    
                    
                    }

                    primeiroOperando = visor;
                    visor = "0";
                    operador = botao;

                    break;

                case "-":
                case "*":
                case ":":
                    primeiroOperando = visor;
                    operador = botao;


                    break;

            }

            //preparar os dados para serem enviados para a View
            ViewBag.Visor = visor;
            ViewBag.PrimeiroOperando = primeiroOperando;
            ViewBag.Operador = operador;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}