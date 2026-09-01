# 🛠️ Gestão de Equipamentos - Academia do Programador 2026

![C#](https://img.shields.io/badge/C%23-.NET%2010.0-blueviolet?style=for-the-badge&logo=csharp)
![Status](https://img.shields.io/badge/Status-Conclu%C3%ADdo-brightgreen?style=for-the-badge)

> Sistema de controle de inventário e gestão de equipamentos e fabricantes, desenvolvido para automatizar e otimizar processos de controle interno de estoque e manutenção.

---

## 📌 Sobre o Projeto

Este projeto foi desenvolvido como parte do treinamento da **[Academia do Programador (Back-End 2026)](https://www.academiadoprogramador.net)**. 

O objetivo principal é solucionar a necessidade do funcionário **Junior**, que necessitava de uma aplicação robusta e estruturada para substituir o controle manual feito em planilhas do Excel. A aplicação gerencia o ciclo de vida completo dos **Fabricantes** e dos **Equipamentos** em estoque.

---

## 🚀 Funcionalidades

### 🏭 1. Controle de Fabricantes
- **Cadastrar Fabricante:** Registro com ID único, nome, e-mail e telefone de contato.
- **Visualizar Fabricantes:** Listagem clara de todos os fabricantes cadastrados.
- **Editar Fabricante:** Atualização completa das informações cadastrais.
- **Excluir Fabricante:** Remoção de fabricantes do sistema.
- **Vínculo com Equipamentos:** Associação direta com os equipamentos cadastrados.

### ⚙️ 2. Controle de Equipamentos
- **Cadastrar Equipamento:** Registro com ID único, nome (mínimo de 3 caracteres), preço de aquisição, data de fabricação e vinculação obrigatória a um fabricante.
- **Visualizar Inventário:** Exibição completa do inventário informando ID, nome, preço, fabricante e data de fabricação.
- **Editar Equipamento:** Atualização de todos os atributos do equipamento.
- **Excluir Equipamento:** Remoção de itens do inventário com atualização automática e em tempo real da listagem.

---

## 🛠️ Tecnologias e Conceitos Utilizados

- **Linguagem:** C# (.NET 10.0)
- **Paradigma:** Programação Orientada a Objetos (POO)
- **Arquitetura:** Camadas bem definidas (Domínio, Repositório e Tela/Apresentação)
- **Estrutura de Dados:** Listas/Coleções dinâmicas para manipulação em memória
- **Validações:** Regras de negócio (ex: tamanho mínimo do nome do equipamento, formato de e-mail e preço positivo)

---

## 📂 Estrutura do Projeto

```text
GestaoDeEquipamentos/
├── src/
├── GestaoEquipamentos.ConsoleApp/
│   ├── ModuloFabricante/
│   │   ├── Fabricante.cs
│   │   ├── RepositorioFabricante.cs
│   │   └── TelaFabricante.cs
│   ├── ModuloEquipamento/
│   │   ├── Equipamento.cs
│   │   ├── RepositorioEquipamento.cs
│   │   └── TelaEquipamento.cs
│   ├── Compartilhado/
│   │   ├── EntidadeBase.cs
│   │   ├── RepositorioBase.cs
│   │   └── TelaBase.cs
│   └── Program.cs
└── README.md