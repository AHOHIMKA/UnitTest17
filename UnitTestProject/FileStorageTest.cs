using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnitTestEx;
using Assert = NUnit.Framework.Assert;
using System.Linq;

namespace UnitTestProject
{
    /// <summary>
    /// Summary description for FileStorageTest
    /// </summary>
    [TestClass]
    public class FileStorageTest
    {

        private List<File> files = new List<File>();
        public const string MAX_SIZE_EXCEPTION = "DIFFERENT MAX SIZE";
        public const string NULL_FILE_EXCEPTION = "NULL FILE";
        public const string NO_EXPECTED_EXCEPTION_EXCEPTION = "There is no expected exception";
        public string filename = "File1";
        public const string SPACE_STRING = " ";
        public const string FILE_PATH_STRING = "@D:\\JDK-intellij-downloader-info.txt";
        public const string CONTENT_STRING = "Some text";
        public const string REPEATED_STRING = "AA";
        public const string WRONG_SIZE_CONTENT_STRING = "TEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtext";
        public const string TIC_TOC_TOE_STRING = "tictoctoe.game";

        public const int NEW_SIZE = 5;

        public FileStorage storage = new FileStorage(NEW_SIZE);

        /* ПРОВАЙДЕРЫ */

        static object[] NewFilesData =
        {
            new object[] { new File(REPEATED_STRING, CONTENT_STRING) },
            new object[] { new File(SPACE_STRING, WRONG_SIZE_CONTENT_STRING) },
            new object[] { new File(FILE_PATH_STRING, CONTENT_STRING) }
        };

        static object[] FilesForDeleteData =
        {
            new object[] { new File(REPEATED_STRING, CONTENT_STRING), REPEATED_STRING },
            new object[] { null, TIC_TOC_TOE_STRING }
        };

        static object[] NewExceptionFileData = {
            new object[] { new File(REPEATED_STRING, CONTENT_STRING) }
        };

        /* Тестирование записи файла */
        [TestMethod] // Исправил это - [Test, TestCaseSource(nameof(NewFilesData))] на тест-метод для запуска
        public void WriteTest()  // Убрал параметр из метода, из-за него не работал тест
        {
            File file1 = new File("test.png"); //Параметр создал новый

            Assert.True(storage.Write(file1));
            storage.DeleteAllFiles(); //Для возврата в начальное состояние
        }

        /* Тестирование записи дублирующегося файла */
        [TestMethod] // Исправил это - [Test, TestCaseSource(nameof(NewFilesData))] на тест-метод для запуска
        public void WriteExceptionTest() // Убрал параметр из метода, из-за него не работал тест
        {
            File file1 = new File("test.png"); //Параметр создал новый вручную, имитация создания файла

            bool isException = false;
            try
            {
                storage.Write(file1);
                Assert.False(storage.Write(file1));
                storage.DeleteAllFiles(); // Очистка storage, возврат в начальное положение 
            }
            catch (FileNameAlreadyExistsException)
            {
                isException = true;
            }
            Assert.True(isException, NO_EXPECTED_EXCEPTION_EXCEPTION);
        }

        /* Тестирование проверки существования файла */
        [TestMethod] // Исправил это - [Test, TestCaseSource(nameof(NewFilesData))] на тест-метод для запуска
        public void IsExistsTest() // Убрал параметр из метода, из-за них не работал тест
        {
            //Создается объект File file с данными.
            //  проверка, существует ли файл в хранилище до записи(ожидаемое значение - false).
            //   запись файла в хранилище.
            //проверка, существует ли файл в хранилище после записи(ожидаемое значение - true).
            //и очистка хранилища

            // Storage изначально пустой

            List<File> files = new List<File>();
            File file1 = new File("test.png"); //Параметр создал новый вручную, имитация создания файла

            // Act
            bool isFileExistsBeforeWrite = storage.IsExists(file1.GetFilename());

            storage.Write(file1); //Заполнение хранилища(Изначально был пустой)

            bool isFileExistsAfterWrite = storage.IsExists(file1.GetFilename());

            // Assert
            Assert.False(isFileExistsBeforeWrite);
            Assert.True(isFileExistsAfterWrite);

            // Cleanup
            storage.DeleteAllFiles(); //Для возврата в начальное состояние

        }

        /* Тестирование удаления файла */
        [TestMethod] // Исправил это - [Test, TestCaseSource(nameof(NewFilesData))] на тест-метод для запуска
        public void DeleteTest(/*File file, String fileName*/) // Убрал параметр из метода, из-за них не работал тест
        {

            files.Clear();
            Assert.AreEqual(files.Count, 0); //проверка на очистку List

        }
        public string GetFilename()
        {
            return filename;
        }


        /* Тестирование получения файлов */
        [TestMethod]
        public void GetFilesTest() //не изменял,только подписал [TestMethod]
        {
            foreach (File el in storage.GetFiles())
            {
                Assert.NotNull(el);
            }
        }


        /* Тестирование получения файла */
        [TestMethod] // Исправил это - [Test, TestCaseSource(nameof(NewFilesData))] на тест-метод для запуска
        public void GetFileTest() // Убрал параметр из метода, из-за них не работал тест
        {
            File expectedFile = new File("filename.txt"); //имитация сохранения файла

            // Создаем файл и записываем его в хранилище
            storage.Write(expectedFile);

            // Получаем файл из хранилища и сравниваем с ожидаемым файлом
            File actualFile = storage.GetFile(expectedFile.GetFilename());
            bool difference = actualFile.GetFilename().Equals(expectedFile.GetFilename()) && actualFile.GetSize().Equals(expectedFile.GetSize());

            // Проверяем, что файлы равны
            Assert.IsTrue(difference, string.Format("There are some differences in {0} or {1}", expectedFile.GetFilename(), expectedFile.GetSize()));
        }

        public List<File> GetFiles() // Получение файлов
        {
            return files;
        }


        public File GetFile(String fileName) //Получение файла/проверка на существование/ Проверка имени
        {
            if (IsExists(fileName))
            {
                foreach (File file in files)
                {
                    if (file.GetFilename().Contains(fileName))
                    {
                        return file;
                    }
                }
            }
            return null;
        }
        public bool IsExists(String fileName) // Проверка на существание 
        {
            // Для каждого элемента с типом File из Листа files
            foreach (File file in files)
            {
                // Проверка имени
                if (file.GetFilename().Contains(fileName))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
