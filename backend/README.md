# Introduction 
ITM v2 features new web layout and re-structured Order Management System.
Knowledge to these frameworks/services are required:
1. VueJS v3
2. .NET Core
3. .NET Framework
4. RabbitMQ

# Getting Started
Pre-Requisites:
- Node.js
- RabbitMQ (along with Erlang)
- .NET Framework v4.8
- .NET Core v3.1

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 




Please follow the rules/conventions as coding standards on this project:
1. Variable on Model that is not present on Database as a Column/Field must be wrapped inside a `#region` and must be written in **lowercase_underline** format to be easily identified. (Ex.: shipment_type_name)
2. API Route should also be written in **lowercase_underline**. (Ex.: refresh_freight_cost)
3. All API should return `ResultData` with few exceptions such as List API.

