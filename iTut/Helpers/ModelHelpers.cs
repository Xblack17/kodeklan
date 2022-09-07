using iTut.Models.Parent;
using iTut.Models.ViewModels.Parent;

namespace iTut.Helpers
{
    public static class ModelHelpers
    {
        public static EditComplaintViewModel GetEditComplaintViewModel(Complaint complaint)
        {
            var editComplaintViewModel = new EditComplaintViewModel
            {
                Id = complaint.Id,
                ParentId = complaint.ParentId,
                Title = complaint.Title,
                ComplaintBody = complaint.ComplaintBody,
                Status = complaint.Status,
                CreateAt = complaint.CreateAt,
                UpdateAt = complaint.UpdateAt,
                Archived = complaint.Archived
            };

            return editComplaintViewModel;
        }
    }
}
