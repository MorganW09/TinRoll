using Tinroll.Data.Entity;
using Tinroll.Model.Dto.Entity;

namespace Tinroll.Business.Mapping
{
    public class UserMapper
    {
        public static UserDto ToDto(User user, bool fullMapping = true) 
        {
            if (user == null) {
                return null;
            }

            return new UserDto {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Description = user.Description,
                CreatedDate = user.CreatedDate,
                ModifiedDate = user.ModifiedDate,
            };
        }

        public static User ToEntity(UserDto userDto, bool fullMapping = true)
        {
            if (userDto == null) {
                return null;
            }

            return new User {
                UserId = userDto.UserId,
                UserName = userDto.UserName,
                Email = userDto.Email,
                Description = userDto.Description,
                CreatedDate = userDto.CreatedDate,
                ModifiedDate = userDto.ModifiedDate,
            };
        }
    }
}