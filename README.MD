
<h1>HybridWork API</h1>

<p>Guilherme Monteiro Espim Rm99499</p>
<p>João Paulo Fonseca Zamperlini Rm99279</p>
<p>Arthur Petrin Rm98735</p>

<h2>Descrição</h2>
<p>
    A HybridWork API é uma solução para gestão de ambientes de trabalho híbrido.
    Permite gerenciar workstations, funcionários, reservas e horários híbridos,
    oferecendo controle completo para ambientes corporativos.
</p>

<h2>Versão da API</h2>
<p>Atualmente implementada: <strong>v1</strong></p>
<p>Exemplo de rota base: <code>/api/v1/[controller]</code></p>

<p>
    <h2>Autenticação</h2>
        A API utiliza JWT para autenticação. Antes de acessar endpoints protegidos,
        é necessário registrar um usuário e realizar login para obter o token.
</p>
<ul>
    <li>Registrar: <code>POST /api/v1/auth/register</code></li>
    <li>Login: <code>POST /api/v1/auth/login</code></li>
    <li>Header Authorization: <code>Bearer &lt;TOKEN&gt;</code></li>
</ul>

<h2>Entidades e Endpoints</h2>

<h3>Workstations</h3>
<ul>
    <li>GET /api/v1/workstations</li>
    <li>GET /api/v1/workstations/{id}</li>
    <li>POST /api/v1/workstations</li>
    <li>PUT /api/v1/workstations/{id}</li>
    <li>DELETE /api/v1/workstations/{id}</li>
</ul>

<h3>Employees</h3>
<ul>
    <li>GET /api/v1/employees</li>
    <li>GET /api/v1/employees/{id}</li>
    <li>POST /api/v1/employees</li>
    <li>PUT /api/v1/employees/{id}</li>
    <li>DELETE /api/v1/employees/{id}</li>
</ul>

<h3>Workspace Reservations</h3>
<ul>
    <li>GET /api/v1/workspacereservations</li>
    <li>GET /api/v1/workspacereservations/{id}</li>
    <li>POST /api/v1/workspacereservations</li>
    <li>PUT /api/v1/workspacereservations/{id}</li>
    <li>DELETE /api/v1/workspacereservations/{id}</li>
</ul>

<h3>Hybrid Schedules</h3>
<ul>
    <li>GET /api/v1/hybridschedules</li>
    <li>GET /api/v1/hybridschedules/{id}</li>
    <li>POST /api/v1/hybridschedules</li>
    <li>PUT /api/v1/hybridschedules/{id}</li>
    <li>DELETE /api/v1/hybridschedules/{id}</li>
</ul>

<h2>Banco de Dados</h2>
<p>
    A API utiliza <strong>MongoDB</strong> para persistência dos dados.  
    As configurações de conexão estão no arquivo <code>appsettings.json</code>:
</p>
<pre>
{
  "MongoDBSettings": {
    "ConnectionURI": "mongodb://localhost:27017",
    "DatabaseName": "HybridWorkDB"
  }
}
</pre>

<h2>Como executar</h2>
<ol>
    <li>Clonar o repositório</li>
    <li>Instalar dependências com <code>dotnet restore</code></li>
    <li>Executar a API com <code>dotnet run</code></li>
    <li>Testar endpoints via Postman ou Swagger</li>
</ol>

<h2>Boas Práticas Implementadas</h2>
<ul>
    <li>Uso correto de verbos HTTP (GET, POST, PUT, DELETE)</li>
    <li>Status codes adequados (200, 201, 204, 400, 404, 401)</li>
    <li>Versionamento da API (/api/v1)</li>
    <li>Autenticação via JWT</li>
    <li>Integração com MongoDB para persistência de dados</li>
</ul>

<h2>Diagrama</h2>

<img src="diagrama.svg">