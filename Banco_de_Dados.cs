1. Crie uma classe Usuário com os atributos, nome, email e senha. Implemente conceitos de construtor, polimorfismo e encapsulamento. 
2. Utilize a classe criada no exercício anterior e construa um método para cadastrar usuários em um banco de cados mysql. 

1. 
Código: 

public class Usuario
{
    public string Nome { get; set; }
    public string Email { get; set; }

    private string senha;
    public string Senha
    {
        get { return senha; }
        set
        {
            // Exemplo de validação
            if (value.Length >= 6)
                senha = value;
            else
                throw new ArgumentException("A senha deve ter no mínimo 6 caracteres.");
        }
    }

    public Usuario()
    {
        Nome = "Desconhecido";
        Email = "sem@email.com";
        Senha = "123456";
    }

    // Construtor com parâmetros (sobrecarga = polimorfismo)
    public Usuario(string nome, string email, string senha)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
    }

    // Método que pode ser sobrescrito (exemplo de polimorfismo com virtual)
    public virtual void ExibirInfo()
    {
        Console.WriteLine($"Nome: {Nome}, Email: {Email}");
    }
}


2 .
Código:

CREATE TABLE usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100),
    email VARCHAR(100),
    senha VARCHAR(100)
);

using MySql.Data.MySqlClient;
using System;

public class UsuarioDAO
{
    private string conexao = "server=localhost;database=sua_base;uid=seu_usuario;pwd=sua_senha;";

    public void CadastrarUsuario(Usuario usuario)
    {
        using (MySqlConnection conn = new MySqlConnection(conexao))
        {
            try
            {
                conn.Open();
                string sql = "INSERT INTO usuarios (nome, email, senha) VALUES (@nome, @email, @senha)";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                    cmd.Parameters.AddWithValue("@email", usuario.Email);
                    cmd.Parameters.AddWithValue("@senha", usuario.Senha);

                    cmd.ExecuteNonQuery();
                }

                Console.WriteLine("Usuário cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar: " + ex.Message);
            }
        }
    }
}
