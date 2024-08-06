# Jws - Json Web Signature

### O que é?

É um padrão de assinatura digital e criptografia para dados Json. É o padrão de assinatura digital do Jwt (Json Web Token), porém não estão atrelados, podendo ser utilizado normalmente para outros usos. 

### Objetivo da construção

Quase sempre que surge a necessidade de assinatura digital de documentos optamos por assinar via Xml, pois possui padrão mais difundido e mais conteúdo na internet, porém, esse modelo exemplifica  o uso da assinatura digital com Json, fornecendo outra opção padronizada.

### Do que é composto o token assinado?

Um token assinado (assim como o Jwt) possui três partes principais:

#### xxxx.yyyy.ppp

#### Cabeçalho (xxxx)

	{
	 "typ": "json",
	 "alg": "RS256"
	}

**Typ**: Intenção do documento, normalmente é um "Jwt".
**Alg**: Criptografia utilizada para a assinatura digital.

#### Dados (yyyy)
	{
	 "Dado1": 1,
	 "Dado2": 2
	}

Representa quais dados serão criptografados.

#### Criptografia (ppp) 
Hash criptografado que garante a integridade de todos os dados..

### Como geramos a hash de assinatura?

Primeiro, convertemos o cabeçalho e os dados em base64Url encode, após isso aplicamos a criptografia escolhida no cabeçalho do token.

CriptografiaEscolhida(base64Url(cabeçalho).base64Url(corpo))

Após todos juntos, formamos o token completo:

base64Url(cabeçalho).base64Url(corpo).CriptografiaEscolhida(base64Url(cabeçalho).base64Url(corpo))

### Base64 x Base64Url

O Base64 permite caracteres não permitidos por Url, o que pode acabar gerando erros, para solucionar isso foi criado o Base64Url onde é basicamente o base64 porém com símbolos alterados. Segue a lista de substituições:

- = : Vazio
- + : -
- / : __

## Aplicação Exemplo

A aplicação desse repositório exemplifica o uso dessa assinatura digital, sendo composta por dois componentes:

#### Cliente
Aplicação console onde é montado os dados do token e assina-o digitalmente utilizando certificado digital.

#### Server
Aplicação de Api que recebe o token e valida-o utilizando uma biblioteca comum de Jwt. 

