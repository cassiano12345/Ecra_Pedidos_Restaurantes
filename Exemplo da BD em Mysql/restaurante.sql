-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 22-Jun-2026 às 19:50
-- Versão do servidor: 10.4.32-MariaDB
-- versão do PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `restaurante`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `pos_mesas`
--

CREATE TABLE `pos_mesas` (
  `id` int(11) NOT NULL,
  `codemp` int(50) NOT NULL,
  `designacao` varchar(200) NOT NULL,
  `nrmenu` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `pos_mesas`
--

INSERT INTO `pos_mesas` (`id`, `codemp`, `designacao`, `nrmenu`) VALUES
(1, 1, 'Sala Principal', '2'),
(2, 1, 'Bar', '1'),
(3, 1, 'Take away', '3');

-- --------------------------------------------------------

--
-- Estrutura da tabela `pos_mesas_pedidos`
--

CREATE TABLE `pos_mesas_pedidos` (
  `id` int(255) NOT NULL,
  `sala` varchar(50) DEFAULT NULL,
  `mesa` varchar(50) NOT NULL,
  `descricao` varchar(200) DEFAULT NULL,
  `qnt` varchar(50) DEFAULT NULL,
  `status` varchar(50) DEFAULT NULL,
  `codemp` int(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `pos_mesas_pedidos`
--

INSERT INTO `pos_mesas_pedidos` (`id`, `sala`, `mesa`, `descricao`, `qnt`, `status`, `codemp`) VALUES
(1, '1', '3', 'Bacalhau a brás', '4', 'E', 1),
(2, '1', '5', 'Café', '1', 'P', 1),
(3, '2', '3', 'Bifanas', '2', 'P', 1),
(4, '3', '', 'Arroz de pato', '5', 'E', 1),
(5, '1', '5', 'Almondegas', '1', 'P', 1);

--
-- Índices para tabelas despejadas
--

--
-- Índices para tabela `pos_mesas`
--
ALTER TABLE `pos_mesas`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `pos_mesas_pedidos`
--
ALTER TABLE `pos_mesas_pedidos`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT de tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `pos_mesas`
--
ALTER TABLE `pos_mesas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de tabela `pos_mesas_pedidos`
--
ALTER TABLE `pos_mesas_pedidos`
  MODIFY `id` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
