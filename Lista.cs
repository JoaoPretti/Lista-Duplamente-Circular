using System;

namespace ricards
{
    class Lista
    {
        public Noh primeiro { get; set; }
        public int tamanho { get; set; }

        public Lista() {
            this.primeiro = null;
            this.tamanho = 0;
        }

        public void Printar()
        {
            if(this.primeiro == null) return;
            Noh aux = this.primeiro;
            do {
                Console.WriteLine(aux.info);
                aux = aux.prox;
            } while(aux != this.primeiro);
        }

        public void Add(Contato contato)
        {
            Noh novoNoh = new Noh(contato);

            if(this.primeiro == null)
            {
                novoNoh.prox = novoNoh.ant = novoNoh;
                this.primeiro = novoNoh;
            }
            else
            {
                Noh ultimo = this.primeiro.ant;
                novoNoh.prox = this.primeiro;
                this.primeiro.ant = novoNoh;
                novoNoh.ant = ultimo;
                ultimo.prox = novoNoh;
            }

            this.tamanho++;
        }

        public bool Deletar(string email)
        {
            if(this.primeiro == null) return false;

            Noh iterador = this.primeiro;
            Noh ant = null;

            while(iterador.info.Email != email)
            {
                if(iterador.prox == this.primeiro) return false;

                ant = iterador;
                iterador = iterador.prox;
            }

            if(iterador.prox == this.primeiro && ant == null)
            {
                this.primeiro = null;
                this.tamanho--;
                return true;
            }

            if(iterador == this.primeiro)
            {
                ant = this.primeiro.ant;
                this.primeiro = this.primeiro.prox;

                ant.prox = this.primeiro;
                this.primeiro.prox = ant;
            }
            else if(iterador.prox == this.primeiro)
            {
                ant.prox = this.primeiro;
                this.primeiro.ant = ant;
            }
            else
            {
                Noh aux = iterador.prox;

                ant.prox = aux;
                aux.ant = ant;
            }
            iterador = null;
            this.tamanho--;
            return true;
        }

        public Contato Encontrar(string email)
        {
            if(this.primeiro == null) return null;
            Noh aux = this.primeiro;
            do {
                if(aux.info.Email == email) return aux.info;
                aux = aux.prox;
            } while(aux != this.primeiro);

            return null;
        }

        public void Ordenar(string tipo)
        {
            if(this.primeiro == null) return;

            bool trocou;
            Noh nohAux;

            do
            {
                trocou = false;
                nohAux = primeiro;

                for(int i = 0; i < this.tamanho - 1; i++)
                {
                    int comparar = tipo == "nome"
                        ? String.Compare(nohAux.info.Nome, nohAux.prox.info.Nome)
                        : String.Compare(nohAux.info.Email, nohAux.prox.info.Email);

                    if (comparar > 0)
                    {
                        Contato contatoAux = nohAux.info;
                        nohAux.info = nohAux.prox.info;
                        nohAux.prox.info = contatoAux;
                        trocou = true;
                    }
                    nohAux = nohAux.prox;
                }
            } while (trocou);
        }

        public void Trocar(Noh a, Noh b)
        {
            Noh[] vetorAux = new Noh[4];
            Noh aux;

            if(b.prox == a)
            {
                aux = a;
                a = b;
                b = aux;
            }

            vetorAux[0] = a.ant;
            vetorAux[1] = b.ant;
            vetorAux[2] = a.prox;
            vetorAux[3] = b.prox;

            if((a.prox == b && b.prox == a) || (a.prox == b && b.prox == a))
            {
                a.ant = vetorAux[2];
                b.ant = vetorAux[0];
                a.prox = vetorAux[3];
                b.prox = vetorAux[1];
            }
            else
            {
                a.ant = vetorAux[1];
                b.ant = vetorAux[0];
                a.prox = vetorAux[3];
                b.prox = vetorAux[2];
            }

            a.ant.prox = a;
            a.prox.ant = a;

            b.ant.prox = b;
            b.prox.ant = b;
        }

        public int Tamanho()
        {
            if(this.primeiro == null) return 0;

            Noh aux = this.primeiro;
            int cont = 0;
            do {
                cont++;
                aux = aux.prox;
            } while(aux != this.primeiro);

            return cont;
        }
    }

    internal class Noh
    {
        internal Contato info;
        internal Noh ant;
        internal Noh prox;

        public Noh(Contato contato)
        {
            this.ant = this.prox = null;
            this.info = contato;
        }
    }
}