   M S B A M L   `   `   `  ���� C  ?Database, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:DatabaseDatabase      Database.MainWindow   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     (localclr-namespace:Database       � Title$  База данных��$	��768��$
��1366�� � 	Resources  �� 
TargetType �� Property �� Value��%/   'o�       �� " o�.���  � ��
 ���� .���  � RowDefinitions �� $	��21* ��� $
��731* �.+�o� $��0,0,0,0q�.@�m� $��Таблицы��.@�m� $"��Товар в наличии�� m� CommandParameter$ AvailableTable�� m� Command �� *
TableClick+m� $#��Проданный товар��$ 	SellTable�� �� *
TableClick+m� $��Заказы��.@�m� $!��Текущие заказы��$
 True�� �� *OpenOrderCommand+m� $��Архив заказов��$ False�� �� *OpenOrderCommand+m� $��
Товар��$ ProductTable�� �� *
TableClick+m� $��Клиенты��$ ClientTable�� �� *
TableClick+m� $��Профиль��$ ProfileTable�� �� *
TableClick+m� $��
Карты��$ 	CardTable�� �� *
TableClick+m� $��Поставщики��$ DeliverTable�� �� *
TableClick+m� $)��#Товары поставщиков��$ DeliverProductTable�� �� *
TableClick+m� $��Excel��.@�m� $-��'Импортировать данные�� �� *ImportDatabase+m� $/��)Экспортировать данные�� �� *ExportDatabase+m� $��Запросы������ *	QueryList+���� " m�.���  � �� ��  �� Path$
 Name���  �@m�Command �� *ExecuteQueryCommand+ � $��1��	  � NavigationUIVisibility$	 Hidden=�$��0,4,0,0q����� *CurrentPage+