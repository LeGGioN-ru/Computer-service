using System.Windows;

namespace Computer_service.Models
{
    public static class UIHelper
    {
        public static bool GetConfirm(string action)
        {
            return MessageBoxResult.Yes == MessageBox.Show($"Вы уверены что хотите {action}? Вы не сможете отменить своё действие.", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Information);
        }

        public static void ShowException(string action)
        {
            MessageBox.Show($"Не удалось {action}. Пожалуйста исправьте ошибку и повторите действие", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
