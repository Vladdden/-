   M S B A M L   `   `   `  ����C  ?WorkForm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:WorkFormWorkForm  5   	       WorkForm.WorkerList   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:WorkForm     5   	   -   5          � Title$  
WorkerList��5   	   $	��455��6   $	��605��6)   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ WorkerListDataGrid��5
         AutoGenerateColumns .  $
��Left=�6K   $	��360��6f   $��	10,10,0,0q�6s   $	��Top=�6�   $	��577��6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Фамилия��6A      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6R   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *	Surname+6R     5      $ Имя��6>   $ 80 6K    6%   �� *Name+6K     5      $ Отчество��6D   $	 100 6V    6%   �� *
Patronymic+6V     5      $ Дата Рождения��6@   $	 140 6W    6%   �� *BthDay+6W     5      $ Пол��6=   $ 35 6J    6%   �� *Sex+6J     5      $# ID Подразделения��6G   $	 110 6a    6%   �� *SubdivisionID+6a   5      �� 5   
   -   5        $��BackBtn��5   !   $��
Назад��6   $
��Left=�60   $��
10,378,0,0q�6K   $	��Top=�6_   $	��100��6w    J� RenderTransformOrigin$ 0.133,0.682&�6�   $��32��6�   6�   �� 5   
   -   5        $��SaveBtn��5   %   $��Сохранить��6   $
��Left=�64   $��480,378,0,0q�6O   $	��Top=�6d   $	��100��6|   $ 0.133,0.682&�6�   $��32��6�   6�   �� 5   
   -   5        $��	UpdateBtn��5   $   $��Обновить��6   $
��Left=�65   $��368,378,0,0q�6P   $	��Top=�6e   $	��100��6}   $ 0.133,0.682&�6�   $��32��6�   6�   5      5      