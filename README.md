# TestConnecter
Этот репозиторий создан для работы над курсовой работой. Если хотите что-нибудь интересное почитать, то зайдите на MyProgram.us

Прежде чем начать использовать данную программу, нужно произвести базовые ее настройки и скинуть файл Connecter2/lang/ru.txt в папку с исполняемым файлом. После этого при запуске программы нужно указать следующие ключи:

Например: --search --mail --http

Здесь 

--search - Настроить утечку через поисковые системы

--mail - Настроить утечку через почтовый сервер

--http - Настроить утечку через Http с/без прокси


Для запуска нужно указать ключи

Например: -mail -http -search -type type[1] type[2] ... -start Start_Folder

Здесь:

-mail - Необязательный ключ для передачи информации через почту

-http - Необязательный ключ для передачи информации через интернет

-search -необязательный ключ для передачи информации через поисковые системы

-type - типы файлов, которые нужно передать (потдерживается до 10 типов (но не тестировалось))

-start - папка, в которой будет искать файлы для передачи

Хотел добавить, что утечка через поиковую систему работает, но в результате часть данных теряется, поэтому восстановить их невозможно.
