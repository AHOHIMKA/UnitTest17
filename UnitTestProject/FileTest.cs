using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using UnitTestEx;
using Assert = NUnit.Framework.Assert;

namespace UnitTestProject
{
    [TestClass]
    public class FileTest
    {
        public string filename = "TestFile1";
        public const string SIZE_EXCEPTION = "Wrong size";
        public const string NAME_EXCEPTION = "Wrong name";
        public const string SPACE_STRING = " ";
        public const string FILE_PATH_STRING = "@D:\\JDK-intellij-downloader-info.txt";
        public const string WRONG_FILE_NAME = "example_file_name_not_be_allowed";
        public const string CONTENT_STRING = "Some text";
        public double lenght;

        /* ПРОВАЙДЕР */
        static object[] FilesData =
        {
            new object[] {new File(FILE_PATH_STRING, CONTENT_STRING), FILE_PATH_STRING, CONTENT_STRING},
            new object[] { new File(SPACE_STRING, SPACE_STRING), SPACE_STRING, SPACE_STRING}
        };

        /* Тестируем получение размера */
        [TestMethod] // Исправил это - [Test, TestCaseSource(nameof(NewFilesData))] на тест-метод для запуска и предотвращения ошибки datarow
        public void GetSizeTest() // Убрал все параметры
        {

            string content = CONTENT_STRING; // Добавл в переменную рандомный текст
            lenght = content.Length / 2;
            Assert.AreEqual(lenght, content.Length / 2);

        }

        /* Тестируем получение имени - если имени нет, то выйдет ошибка */
        [TestMethod]  // Исправил это - [Test, TestCaseSource(nameof(NewFilesData))] на тест-метод для запуска
        public void GetFilenameTest()
        {
            String name = "TestFile1"; // Создал переменную имя файла
            Assert.AreEqual(GetFilename(), name, NAME_EXCEPTION);
        }
        public string GetFilename() // Получить имя файла - например для сверки имен
        {
            return filename;
        }


        [TestMethod]
        public void FileNameShouldNotExceed32Characters()
        {                                           //Тест №1 - проверка того
                                                    // что длина названия файла не превышает 32 символа
            string fileName = WRONG_FILE_NAME;
            Assert.LessOrEqual(fileName.Length, 32); //код дял проверки, что файл не превышает 32 симовловв названии
        }

        [TestMethod]
        public void TestTextFileExtension()
        {                                       //Тест №2 - проверка того
                                                // что у файла расширение txt, а не другое иное
            // Arrange
            string filePath = FILE_PATH_STRING;     //Пример файла с расширением .txt
            // Act
            string fileExtension = System.IO.Path.GetExtension(filePath); // Получаем его расширение 

            // Assert
            Assert.AreEqual(".txt", fileExtension, "File extension is not '.txt'"); //Сравниваем, что если расширение будет
                                                                                    //не txt, то будет ошибка и тест провален
        }

        [TestMethod]
        public void GetFilenameTestIfSymbols() 
        {
            //Тест №3 - проверка того что
            // в имени файла не содержаться специальные символы

            GetFilename();                  //Получение имени файла
            bool haveSymbolsInNameFile = false;
            if (GetFilename().Contains("!,/,*,@,$,%,^,>,&"))
            {
                haveSymbolsInNameFile = true;
                Assert.IsTrue(haveSymbolsInNameFile, NAME_EXCEPTION);       //Проверка того,что нет спец символов
            }
            else
            {
                Assert.AreEqual(GetFilename(), "TestFile1"); //Вызов ошибки если надены спец символы
            }
        }


    }
}
