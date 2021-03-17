using System;
/*
Entrega de trabalho
Nós,
Lucas Fernando Soares Morgado de Souza
Larissa Teixeira Araújo
declaramos que
todas as respostas são fruto de nosso próprio trabalho,
não copiamos respostas de colegas externos à equipe,
não disponibilizamos nossas respostas para colegas externos à equipe e
não realizamos quaisquer outras atividades desonestas para nos beneficiar
ou prejudicar outros.
*/
namespace Expressoes
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Versao infixa (eh pra botar ai): ");
                string infixa = Console.ReadLine();
                string posfixs = Convert(infixa);

                Console.WriteLine("Infixa convertida para posfixa: " + posfixs);
                Console.WriteLine("O resultado do calculo da posfixa é:  " + Calcula(posfixs));
                Console.WriteLine("");
            } 
        }

        private static float Calcula (string posfixs)
        {   
            float resultado = 0;
            Pilha<float> calcula = new Pilha<float>(posfixs.Length);

            for (int i = 0; i <posfixs.Length; i++)
            {
                //Nessa sequencia de if's e else if's, eu comparo com cada simbolo e calculo de acordo com
                string atual = Char.ToString(posfixs[i]);
                if (atual == "+")
                {
                    float soma = calcula.pop() + calcula.pop();
                    calcula.push(soma);

                }
                else if (atual == "-")
                {
                    float subtrai = calcula.pop() - calcula.pop();
                    calcula.push(subtrai);

                }
                else if (atual == "*")
                {
                    float vezes = calcula.pop() * calcula.pop();
                    calcula.push(vezes);

                }
                else if (atual == "/")
                {
                    float temp = calcula.pop(); 
                    float divide = calcula.pop() / temp;
                    calcula.push(divide);

                }
                else if (atual == "%")
                {
                    float temp = calcula.pop();
                    float resto = calcula.pop() % temp;
                    calcula.push(resto);

                }
                else if (atual == "^")
                {
                    float temp = calcula.pop();
                    float elevaodo = MathF.Pow (calcula.pop(),temp);
                    calcula.push(elevaodo);

                }
                else
                    calcula.push(float.Parse(atual));
            }
            resultado += calcula.pop();
            return  resultado;
        }
        private static string Convert (string infixa)
        {
            string posfixa = "";
            Pilha<string> guardar = new Pilha<string>(infixa.Length);

            for (int i = 0; i < infixa.Length; i++)
            {
                string atual = Char.ToString(infixa[i]);
                if (atual != "+" && atual != "-" && atual != "/" && atual != "%" && atual != "^" && atual != "*" && atual != "(" && atual != ")") //testo se eh numero ou simbolo
                {
                    posfixa += atual;
                } else
                {
                    //verifico se a maior prioridade ja esta na pilha
                    if (atual == "^") 
                    {
                        //verifico se o ultimo colocado eh igual ao atual, se for, preciso esvaziar a pilha antes
                        while (!guardar.isEmpty())
                        {
                            if (guardar.GetUltimoColocado() == "^")
                            {
                                //esvazio a posicao que tem a prioridade igual
                                posfixa += guardar.pop();
                            } else
                            {
                                break;
                            }
                        }
                        guardar.push(atual); //e entao adiciono na pilha
                    } else if (atual == "*" || atual == "/" || atual == "%" ){ //verifico se eh a de media prioridade ou maior
                        while (!guardar.isEmpty())
                        {
                            if (guardar.GetUltimoColocado() == "*" || guardar.GetUltimoColocado() == "/" || guardar.GetUltimoColocado() == "%" || guardar.GetUltimoColocado() == "^") //verifico tambem se o ultimo colocado nao eh semelhante a este nivel
                            {
                                //esvazio a pilha da mesma prioridade ou maior
                                posfixa += guardar.pop();
                            } else
                            {
                                break;
                            }
                        }
                        //se nao for, eu atualizo o ultimoColocado e coloco na pilha
                        guardar.push(atual);
                    } else if (atual == "+" || atual == "-") { //verifico se eh a de menor prioridade ou maior
                        while (!guardar.isEmpty())
                        {
                            //caso seja o ultimoColocado seja maior ou igual, esvazio toda a pilha caso eu ache alguma da mesma prioridade ou maior
                            if (guardar.GetUltimoColocado() == "*" || guardar.GetUltimoColocado() == "/" || guardar.GetUltimoColocado() == "%" || guardar.GetUltimoColocado() == "^" || guardar.GetUltimoColocado() == "+" || guardar.GetUltimoColocado() == "-")
                            {                     
                                posfixa += guardar.pop();
                            } else
                            {
                                break;
                            }
                        }
                        //por fim eu guardo o simbolo e guardo o simbolo colocado
                        guardar.push(atual);
                    } else if (atual == "(") {
                        guardar.push(atual);
                    } else if (atual == ")") {
                        while (guardar.GetUltimoColocado() != "(")
                        {
                            posfixa += guardar.pop();
                        }
                        //o while ja vai achar por conta propria, entao eh so dar pop vazio, pra n ser adicionado na posfixa
                        guardar.pop();
                    }
                }
            }
            //enquanto a pilha tiver conteudo, concatena no posfixa e tira da pilha    
            while (!guardar.isEmpty()) 
            {
               posfixa += guardar.pop();
            }
            return posfixa;
        }
    }
    class Pilha<Type>
    {
        // Um TAD Pilha teria os atributos com visibilidade interna (encapsulados). 
        private Type[] elementos;
        private int topo;
        // para inicializar os atributos teremos o contrutor
        public Pilha(int tam)
        {
            this.elementos = new Type[tam];
            this.topo = -1; // pilha esta vazia
        }
        public void push(Type elemento)
        {
            // se a pilha esta cheia gera uma excecao
            if (this.isFull())
                throw new Exception("Pilha cheia.");

            this.topo++;
            elementos[topo] = elemento;
        }
        public Type pop()
        {
            // se a pilha estiver vazia gera uma excecao
            if (this.isEmpty())
                throw new Exception("Pilha vazia.");

            Type ch = elementos[topo];
            topo--;
            return ch;
        }
        public bool isEmpty()
        {
            return topo == -1;
        }
        public bool isFull()
        {
            return topo >= elementos.Length - 1;
        }
        public Type GetUltimoColocado ()
        {
            //pego e mando o ultimo colocado da lista
            return elementos[topo];
        }
    }
}
