-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema loja_esportiva
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema loja_esportiva
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `loja_esportiva` ;
USE `loja_esportiva` ;

-- -----------------------------------------------------
-- Table `loja_esportiva`.`TB_CARRINHO`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `loja_esportiva`.`TB_CARRINHO` (
  `ID_CARRINHO` INT NOT NULL AUTO_INCREMENT,
  `DATA_CRIACAO` DATETIME NULL,
  PRIMARY KEY (`ID_CARRINHO`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `loja_esportiva`.`TB_CLIENTE`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `loja_esportiva`.`TB_CLIENTE` (
  `ID_CLIENTE` INT NOT NULL AUTO_INCREMENT,
  `ID_CARRINHO` INT NOT NULL,
  `NOME_COMPLETO` VARCHAR(100) NOT NULL,
  `CPF` CHAR(15) NOT NULL,
  `ENDERECO_COMPLETO` VARCHAR(300) NOT NULL,
  `DATA_NASCIMENTO` DATE NOT NULL,
  `TELEFONE` CHAR(15) NOT NULL,
  PRIMARY KEY (`ID_CLIENTE`, `ID_CARRINHO`),
  INDEX `CARRINHO_idx` (`ID_CARRINHO` ASC) VISIBLE,
  CONSTRAINT `FK_ID_CARRINHO`
    FOREIGN KEY (`ID_CARRINHO`)
    REFERENCES `loja_esportiva`.`TB_CARRINHO` (`ID_CARRINHO`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `loja_esportiva`.`TB_PEDIDO`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `loja_esportiva`.`TB_PEDIDO` (
  `ID_PEDIDO` INT NOT NULL AUTO_INCREMENT,
  `ID_CLIENTE` INT NOT NULL,
  `VALOR_TOTAL` DECIMAL(10,2) NOT NULL,
  `DATA` DATETIME NOT NULL,
  `STATUS` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ID_PEDIDO`),
  INDEX `CLIENTE_PEDIDO_idx` (`ID_CLIENTE` ASC) INVISIBLE,
  CONSTRAINT `ID_PEDIDO_CLIENTE`
    FOREIGN KEY (`ID_CLIENTE`)
    REFERENCES `loja_esportiva`.`TB_CLIENTE` (`ID_CLIENTE`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `loja_esportiva`.`TB_PAGAMENTO`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `loja_esportiva`.`TB_PAGAMENTO` (
  `ID_PAGAMENTO` INT NOT NULL AUTO_INCREMENT,
  `ID_PEDIDO` INT NOT NULL,
  `TIPO` VARCHAR(45) NOT NULL,
  `VALOR` DECIMAL(10,2) NOT NULL,
  `STATUS` VARCHAR(45) NOT NULL,
  `DATA` DATETIME NOT NULL,
  PRIMARY KEY (`ID_PAGAMENTO`),
  INDEX `PEDIDO_PAGAMENTO_idx` (`ID_PEDIDO` ASC) INVISIBLE,
  CONSTRAINT `FK_ID_PEDIDO_PAGAMENTO`
    FOREIGN KEY (`ID_PEDIDO`)
    REFERENCES `loja_esportiva`.`TB_PEDIDO` (`ID_PEDIDO`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `loja_esportiva`.`TB_ENTREGA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `loja_esportiva`.`TB_ENTREGA` (
  `ID_ENTREGA` INT NOT NULL AUTO_INCREMENT,
  `ID_PEDIDO` INT NOT NULL,
  `DATA` DATE NOT NULL,
  `ENDERECO_COMPLETO` VARCHAR(300) NOT NULL,
  `STATUS` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ID_ENTREGA`),
  INDEX `PEDIDO_ENTREGA_idx` (`ID_PEDIDO` ASC) VISIBLE,
  CONSTRAINT `FK_ID_PEDIDO_ENTREGA`
    FOREIGN KEY (`ID_PEDIDO`)
    REFERENCES `loja_esportiva`.`TB_PEDIDO` (`ID_PEDIDO`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `loja_esportiva`.`TB_MARCA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `loja_esportiva`.`TB_MARCA` (
  `ID_MARCA` INT NOT NULL AUTO_INCREMENT,
  `PAIS_ORIGEM` VARCHAR(45) NOT NULL,
  `NOME` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ID_MARCA`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `loja_esportiva`.`TB_PRODUTO`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `loja_esportiva`.`TB_PRODUTO` (
  `ID_PRODUTO` INT NOT NULL AUTO_INCREMENT,
  `ID_MARCA` INT NOT NULL,
  `NOME` VARCHAR(100) NOT NULL,
  `PESO` DECIMAL(6,2) NULL,
  `COR` VARCHAR(45) NOT NULL,
  `PRECO` DECIMAL(10,2) NOT NULL,
  `DESCRICAO` TEXT NOT NULL,
  `TAMANHO` VARCHAR(45) NOT NULL,
  `CODIGO_BARRAS` VARCHAR(45) NULL,
  `QUANTIDADE_ESTOQUE` INT NOT NULL,
  PRIMARY KEY (`ID_PRODUTO`),
  INDEX `PRODUTO_MARCA_idx` (`ID_MARCA` ASC) VISIBLE,
  CONSTRAINT `FK_ID_MARCA`
    FOREIGN KEY (`ID_MARCA`)
    REFERENCES `loja_esportiva`.`TB_MARCA` (`ID_MARCA`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `loja_esportiva`.`TB_ITEM_CARRINHO`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `loja_esportiva`.`TB_ITEM_CARRINHO` (
  `ID_CARRINHO` INT NOT NULL,
  `ID_PRODUTO` INT NOT NULL,
  `QUANTIDADE` INT NOT NULL,
  `VALOR_UNITARIO` DECIMAL(10,2) NOT NULL,
  `DATA_ADICAO` DATE NOT NULL,
  INDEX `ITEM_CARRINHO_PRODUTO_idx` (`ID_PRODUTO` ASC) VISIBLE,
  INDEX `ITEM_CARRINHO_CARRINHO_idx` (`ID_CARRINHO` ASC) VISIBLE,
  CONSTRAINT `FK_ID_ITEM_CARRINHO`
    FOREIGN KEY (`ID_CARRINHO`)
    REFERENCES `loja_esportiva`.`TB_CARRINHO` (`ID_CARRINHO`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_ID_ITEM_PRODUTO`
    FOREIGN KEY (`ID_PRODUTO`)
    REFERENCES `loja_esportiva`.`TB_PRODUTO` (`ID_PRODUTO`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `loja_esportiva`.`TB_AVALIACAO`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `loja_esportiva`.`TB_AVALIACAO` (
  `ID_CLIENTE` INT NOT NULL,
  `ID_PRODUTO` INT NOT NULL,
  `NOTA` TINYINT NULL,
  `COMENTARIO` TEXT NULL,
  `DATA` DATETIME NULL,
  INDEX `AVALIACAO_CLIENTE_idx` (`ID_CLIENTE` ASC) VISIBLE,
  INDEX `AVALIACAO_PRODUTO_idx` (`ID_PRODUTO` ASC) VISIBLE,
  CONSTRAINT `FK_ID_CLIENTE_AVALIACAO`
    FOREIGN KEY (`ID_CLIENTE`)
    REFERENCES `loja_esportiva`.`TB_CLIENTE` (`ID_CLIENTE`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_ID_PRODUTO_AVALIACAO`
    FOREIGN KEY (`ID_PRODUTO`)
    REFERENCES `loja_esportiva`.`TB_PRODUTO` (`ID_PRODUTO`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `loja_esportiva`.`TB_CATEGORIA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `loja_esportiva`.`TB_CATEGORIA` (
  `ID_CATEGORIA` INT NOT NULL AUTO_INCREMENT,
  `NOME` VARCHAR(45) NOT NULL,
  `DESCRICAO` TEXT NULL,
  PRIMARY KEY (`ID_CATEGORIA`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `loja_esportiva`.`TB_FORNECEDOR`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `loja_esportiva`.`TB_FORNECEDOR` (
  `ID_FORNECEDOR` INT NOT NULL,
  `RAZAO_SOCIAL` VARCHAR(100) NOT NULL,
  `CNPJ` CHAR(18) NOT NULL,
  `EMAIL` VARCHAR(100) NOT NULL,
  `TELEFONE` VARCHAR(15) NOT NULL,
  `ENDERECO_COMPLETO` VARCHAR(300) NOT NULL,
  PRIMARY KEY (`ID_FORNECEDOR`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `loja_esportiva`.`TB_FORNECEDOR_PRODUTO`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `loja_esportiva`.`TB_FORNECEDOR_PRODUTO` (
  `ID_FORNECEDOR` INT NOT NULL,
  `ID_PRODUTO` INT NOT NULL,
  `PRECO` DECIMAL(10,2) NOT NULL,
  `PRAZO_ENTREGA` INT NOT NULL,
  `ULTIMO_FORNECIMENTO` DATE NOT NULL,
  INDEX `ID_FORNECEDOR_idx` (`ID_FORNECEDOR` ASC) VISIBLE,
  INDEX `ID_PRODUTO_FORNECDOR_idx` (`ID_PRODUTO` ASC) VISIBLE,
  CONSTRAINT `FK_ID_FORNECEDOR`
    FOREIGN KEY (`ID_FORNECEDOR`)
    REFERENCES `loja_esportiva`.`TB_FORNECEDOR` (`ID_FORNECEDOR`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_ID_PRODUTO_FORNECDOR`
    FOREIGN KEY (`ID_PRODUTO`)
    REFERENCES `loja_esportiva`.`TB_PRODUTO` (`ID_PRODUTO`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `loja_esportiva`.`TB_PRODUTO_CATEGORIA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `loja_esportiva`.`TB_PRODUTO_CATEGORIA` (
  `ID_PRODUTO` INT NOT NULL,
  `ID_CATEGORIA` INT NOT NULL,
  INDEX `PRODUTO_CATEGORIA_idx` (`ID_PRODUTO` ASC) VISIBLE,
  INDEX `CATEGORIA_PRODUTO_idx` (`ID_CATEGORIA` ASC) VISIBLE,
  CONSTRAINT `FK_ID_PRODUTO_CATEGORIA`
    FOREIGN KEY (`ID_PRODUTO`)
    REFERENCES `loja_esportiva`.`TB_PRODUTO` (`ID_PRODUTO`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_ID_CATEGORIA_PRODUTO`
    FOREIGN KEY (`ID_CATEGORIA`)
    REFERENCES `loja_esportiva`.`TB_CATEGORIA` (`ID_CATEGORIA`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `loja_esportiva`.`TB_ITEM_PEDIDO`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `loja_esportiva`.`TB_ITEM_PEDIDO` (
  `ID_ITEM_PEDIDO` INT NOT NULL AUTO_INCREMENT,
  `ID_PEDIDO` INT NOT NULL,
  `QUANTIDADE` INT NOT NULL,
  `DATA_ADICAO` DATETIME NOT NULL,
  `VALOR_UNITARIO` DECIMAL(10,2) NOT NULL,
  PRIMARY KEY (`ID_ITEM_PEDIDO`),
  INDEX `PEDIDO_ITEM_PEDIDO_idx` (`ID_PEDIDO` ASC) VISIBLE,
  CONSTRAINT `PEDIDO_ITEM_PEDIDO`
    FOREIGN KEY (`ID_PEDIDO`)
    REFERENCES `loja_esportiva`.`TB_PEDIDO` (`ID_PEDIDO`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
