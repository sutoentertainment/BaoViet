using BaoVietCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalRateReminder;

namespace BaoVietCore.Services
{
    public class RateUsService : ServiceBase, IRateUsService
    {
        public RateUsService(Manager man) : base(man)
        {

        }

        public async Task ShowRatePopup(int LaunchLimit = 5, bool ResetCountOnNewVersion = true, string RateButtonText = "rate 5 stars", string CancelButtonText = "no, thanks", string Title = "Rate us!", string Content = "Your feedback helps us improve this app. If you like it, please take a minute and rate it with five stars so we can continue working on new features and updates.")
        {
            Configurate(LaunchLimit, ResetCountOnNewVersion, RateButtonText, CancelButtonText, Title, Content);
            var result = await RatePopup.CheckRateReminderAsync();
            if (result == RateReminderResult.Dismissed)
            {
                await ShowFeedbackPopup();
            }
        }

        public async Task ShowFeedbackPopup()
        {
            FeedbackPopup.ContactEmail = "thang2410199@gmail.com";
            FeedbackPopup.EmailSubject = "[Báo Việt] Feedback";
            FeedbackPopup.SendFeedbackButtonText = "gửi phản hồi";
            FeedbackPopup.CancelButtonText = "để lần sau";
            FeedbackPopup.Title = "gửi phản hồi";
            FeedbackPopup.Content = "Phần mềm còn điểm gì khiến bạn chưa hài lòng? Mọi ý kiến đóng góp của bạn sẽ được nhóm sử dụng để cải thiện phần mềm.";
            await FeedbackPopup.ShowFeedbackDialogAsync();
        }

        public void Configurate(int LaunchLimit, bool ResetCountOnNewVersion, string RateButtonText, string CancelButtonText, string Title, string Content)
        {
            RatePopup.LaunchLimit = LaunchLimit;
            RatePopup.ResetCountOnNewVersion = ResetCountOnNewVersion;
            RatePopup.RateButtonText = RateButtonText;
            RatePopup.CancelButtonText = CancelButtonText;
            RatePopup.Title = Title;
            RatePopup.Content = Content;
        }

        public void ResetCounter()
        {
            RatePopup.ResetLaunchCount();
        }
    }
}
