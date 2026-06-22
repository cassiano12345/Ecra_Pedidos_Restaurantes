### 📧Ercã de mesa e pedidos para restaurantes
O presente projeto apresenta uma solução para restaurantes e bares, em forma de ecrã apresenta para os coznheiros e garçons a mesa e o pedido feito pelo cliente. 

### Algumas funcionalidades a destacar
Ficheiro-> ... <br/>

***Funções***


***Variáveis*** <br/>
Ficheiro->Ercã de pedidos restaurante c# ->Form1.cs
- ConnectionString: É a variavel destinada a guardar os dados da conexão a base de dados.
- _oracleChangeNotifier: É uma variavel que guarda uma instancia da classe "OracleChangeNotifier", com ela é possivel chamar metodos como: StartListening(), selecionar_sala(), Pedido_Executado(), Delete_Pedido(), StopListening().
- dados: É uma variavel do tipo "MeusDados" da classe "MeusDados", que serve para guardar os dados dos pedidos dos clientes proveninetes da base da base de dados. <br/>

Ficheiro->Ercã de pedidos restaurante c# -> OracleChangeNotifier.cs
- Descricao: É uma variavel da classe "MeusDados", e serve para guardar a descrição do pedidos dos clientes
- mesa: É uma variavel da classe "MeusDados", e serve para guardar o numero da mesa dos clientes
- sala: É uma variavel da classe "MeusDados", e serve para guardar o numero da sala dos clientes, que mais tarde sera feita uma pesquisa pelo nome da sala na tabela pos_mesas.
- status: É uma variavel da classe "MeusDados", e serve para guardar o estado do pedido, ou seja se ja foi enviado o pedido aparece em verde, se não foi enviado o pedido aparece em vermelho.
- quantidade:É uma variavel da classe "MeusDados", e serve para guardar a quantidade de pedidos ou pratos pedidos pelo cliente.

***Links***
