   M S B A M L   `   `   `  ����K  GHistogram_ver5.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Histogram_ver5._0Histogram_ver5._0  5   	   U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5      /    )Histogram_ver5._0.ImageAndHistogramWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Histogram_ver5._0     5   	   Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5        � Title$  ImageAndHistogramWindow��5	   	   $	��612��6*   $
��1088��67   .��5
      �  � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $
��auto �6   �� 5      $��1* �6    � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      .+�5   
   � $��0��6   $��0��6   $��3��6,    5      �� 5      $��4* �6#   �� 5      $��* �6#   .+�5      �� -   5         ��Name$ Image_Show_IMG��5      $��0��5      �� 5      -   5         ��Name$ Image_Legend_SP��5      5      5      �� 5    
   $��1��6   $��0��6!   $��3��61   � 5!   
   $��0��6   $��2��6   .+�5"      #  LiveCharts.Wpf.CartesianChart  -   5          Name$ 
FirstGraph��5"   !   $��5q�65   &   LiveCharts.Wpf.Charts.Base.Chart   DisableAnimations .    AxisY 5$        LiveCharts.Wpf.Axis     MinValue$ 0Q�6   	   MaxValue$
	 2.55Q�6,   5&      5'      � 5(   
   $��1��6   $��2��6   .+�5)        -   5        $ SecondGraph��5)   !   $��5q�66    .  5+        $ 0Q�6   $
	 2.55Q�6,   5-      5.      � 5/   
   $��2��6   $��2��6   .+�50        -   5        $ 
ThirdGraph��50   !   $��5q�65    .  52        $ 0Q�6   $
	 2.55Q�6,   54      55      56      57      