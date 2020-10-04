namespace ricards
{
    class Contato
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public Contato(string nome, string email, string telefone)
        {
            this.Nome = nome;
            this.Email = email;
            this.Telefone = telefone;
        }

        public override string ToString()
        {
            return $"=> Contato\nNome: {this.Nome}\nE-mail: {this.Email}\nTelefone: {this.Telefone}";
        }
    }
}
