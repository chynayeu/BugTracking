using BugTracking.DAL.Entities;
using BugTracking.Models;

namespace BugTracking.Services.Impl.Converters
{
    public class DBCommentModelConverter : IConverter<Comment, CommentModel>
    {

        private readonly IConverter<User, UserModel> _userConverter;
        public DBCommentModelConverter(IConverter<User, UserModel> userConverter)
        {
            _userConverter = userConverter;
        }
        public Comment Convert(CommentModel source)
        {
            Comment comment = new Comment();
            comment.Id = source.Id;
            comment.text = source.text;
            comment.date = source.date;
            comment.User = _userConverter.Convert(source.User);
            return comment;
        }

        public CommentModel Convert(Comment source)
        {
            CommentModel comment = new CommentModel();
            comment.Id = source.Id;
            comment.text = source.text;
            comment.date = source.date;
            comment.User = _userConverter.Convert(source.User);
            return comment;
        }
    }
}
