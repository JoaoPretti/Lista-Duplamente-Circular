using System;
using System.IO;
using System.Text;

namespace ricards
{
    class Program
    {
        static void Main(string[] args)
        {
            Lista lista = new Lista();
            string opcao;
            do
            {
                Console.WriteLine("=====[ Menu ]=====");
                Console.WriteLine("1 - Adicionar\n");
                Console.WriteLine("2 - Remover\n");
                Console.WriteLine("3 - Atualizar\n");
                Console.WriteLine("4 - Recuperar\n");
                Console.WriteLine("5 - Ordenar\n");
                Console.WriteLine("6 - Listar\n");
                Console.WriteLine("7 - Salvar em Arquivo\n");
                Console.WriteLine("8 - Navegação\n");
                Console.WriteLine("0 - Sair\n");
                opcao = Console.ReadKey().Key.ToString();
                Console.Clear();
                switch(opcao)
                {
                    case "D1":
                        Console.WriteLine("=====[ Adicionar ]=====");
                        Adicionar(lista);
                        break;
                    case "D2":
                        Console.WriteLine("=====[ Remover ]=====");
                        Remover(lista);
                        break;
                    case "D3":
                        Console.WriteLine("=====[ Atualizar ]=====");
                        Atualizar(lista);
                        break;
                    case "D4":
                        Console.WriteLine("=====[ Recuperar ]=====");
                        Recuperar(lista);
                        break;
                    case "D5":
                        Console.WriteLine("=====[ Ordenar ]=====");
                        Ordenar(lista);
                        break;
                    case "D6":
                        Console.WriteLine("=====[ Listar ]=====");
                        Listar(lista);
                        break;
                    case "D7":
                        Console.WriteLine("=====[ Salvar em Arquivo ]=====");
                        SalvarEmArquivo(lista);
                        break;
                    case "D8":
                        Console.WriteLine("=====[ Navegação ]=====");
                        Navegacao(lista);
                        break;
                }
            } while(opcao != "D0");

            Console.WriteLine("FIM!");
        }

        public static void Adicionar(Lista lista)
        {
            string nome;
            string email;
            string telefone;

            Console.WriteLine("Nome: ");
            nome = Console.ReadLine();
            Console.WriteLine("E-mail: ");
            email = Console.ReadLine();
            Console.WriteLine("Telefone: ");
            telefone = Console.ReadLine();

            lista.Add(new Contato(nome, email, telefone));
            Console.Clear();
            Console.WriteLine("Contato adicionado com sucesso!");
        }

        public static void Remover(Lista lista)
        {
            string email;
            Console.WriteLine("Digite o email: ");
            email = Console.ReadLine();
            if(lista.Deletar(email))
            {
                Console.WriteLine("Contato deletado com sucesso");
            }
            else
            {
                Console.WriteLine("Não foi possível deletar o contato");
            }
        }

        public static void Atualizar(Lista lista)
        {
            string email;
            Console.WriteLine("Digite o email: ");
            email = Console.ReadLine();
            Contato contato = lista.Encontrar(email);
            if(contato == null)
            {
                Console.WriteLine("Contato não encontrado");
            }
            else
            {
                string novoNome;
                string novoEmail;
                string novoTelefone;

                Console.WriteLine("=> Editando Contato");
                Console.WriteLine("Deixe em branco para nao modificar o dado");

                Console.WriteLine($"Nome({contato.Nome}): ");
                novoNome = Console.ReadLine();
                if(!string.IsNullOrEmpty(novoNome)) contato.Nome = novoNome;

                Console.WriteLine($"E-mail({contato.Email}): ");
                novoEmail = Console.ReadLine();
                if(!string.IsNullOrEmpty(novoEmail)) contato.Email = novoEmail;

                Console.WriteLine($"Telefone({contato.Telefone}): ");
                novoTelefone = Console.ReadLine();
                if(!string.IsNullOrEmpty(novoTelefone)) contato.Telefone = novoTelefone;

                Console.WriteLine("Contato atualizado com sucesso.");
            }
        }

        public static void Recuperar(Lista lista)
        {
            try
            {
                using (StreamReader file = new StreamReader("lista.txt"))
                {
                    string nome;
                    string email;
                    string telefone;
                    while(true)
                    {
                        nome = file.ReadLine();
                        if(nome == null) break;
                        email = file.ReadLine();
                        if(email == null) break;
                        telefone = file.ReadLine();
                        if(telefone == null) break;
                        lista.Add(new Contato(nome, email, telefone));
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void Ordenar(Lista lista)
        {
            string ordenarPor;
            do
            {
                Console.Clear();
                Console.WriteLine("Ordenar Por:");
                Console.WriteLine("1 - Nome");
                Console.WriteLine("2 - E-mail");
                Console.WriteLine("0 - Sair");
                ordenarPor = Console.ReadKey().Key.ToString();

                if(ordenarPor == "D1") lista.Ordenar("nome");
                else if(ordenarPor == "D2") lista.Ordenar("e-mail");
            } while(ordenarPor == "D0");
            Console.Clear();
            Console.WriteLine("Ordenado com sucesso\n");
        }

        public static void Listar(Lista lista)
        {
            lista.Printar();
            Console.WriteLine();
        }

        public static void SalvarEmArquivo(Lista lista)
        {
            try
            {
                using (FileStream fs = File.Create("lista.txt"))
                {
                    Noh aux = lista.primeiro;
                    if(aux == null) return;

                    while(aux.prox != lista.primeiro) {
                        AddText(fs, $"{aux.info.Nome}\n{aux.info.Email}\n{aux.info.Telefone}\n");
                        aux = aux.prox;
                    }
                    AddText(fs, $"{aux.info.Nome}\n{aux.info.Email}\n{aux.info.Telefone}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void Navegacao(Lista lista)
        {
            Noh iterador = lista.primeiro;
            if(iterador == null) return;
            while(true)
            {
                Console.Clear();
                Console.WriteLine("=====[ Navegação ]=====");
                Console.WriteLine("=> Seta p/ esquerda: anterior");
                Console.WriteLine("=> Seta p/ direita: próximo");
                Console.WriteLine("=> Barra de Espaço: sair\n");
                Console.WriteLine(iterador.info);
                string tecla = Console.ReadKey().Key.ToString();
                if(tecla == "LeftArrow")
                {
                    iterador = iterador.ant;
                }
                else if(tecla == "RightArrow")
                {
                    iterador = iterador.prox;
                }
                else if(tecla == "Spacebar")
                {
                    break;
                }
            }
            Console.Clear();
        }

        public static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }
}
