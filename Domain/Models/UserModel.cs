using DataAccess.DBServices;
using DataAccess.DBServices.Entities;
using DataAccess.MailServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class UserModel
    {
        #region -> Atributos
        private int _id;
        private string _userName;
        private string _password;
        private string _firstName;
        private string _lastName;
        private string _position;
        private string _email;
        private byte[] _photo;

        private UserRepository _userRepository;
        #endregion

        #region -> Constructores
        public UserModel()
        {
            _userRepository = new UserRepository();
        }

        public UserModel(int id, string userName, string password, string firstName, string lastName, string position, string email, byte[] photo)
        {
            Id = id;
            Username = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Position = position;
            Email = email;
            Photo = photo;

            _userRepository = new UserRepository();
        }
        #endregion

        #region -> Propiedades + Validação e Visualizacão de Dados
        //Posição 0 
        [DisplayName("ID")]
        [Browsable(true)]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        //Posição 1 
        [DisplayName("Usuário")]
        [Required(ErrorMessage = "Informe o Login do usuário.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O Login do usuário deve conter um mínimo de 5 caracteres.")]
        public string Username
        {
            get { return _userName; }
            set { _userName = value; }
        }

        //Posição 2
        [DisplayName("Senha")]
        [Browsable(false)]
        [Required(ErrorMessage = "Por favor informe uma senha.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "A senha deve conter um mínimo de 5 caracteres.")]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        //Posição 3
        [DisplayName("Nome")]
        [Browsable(false)]
        [Required(ErrorMessage = "Por favor informe o nome")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "O nome deve conter somente letras")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve conter um mínimo de 3 caracteres.")]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        //Posição 4
        [DisplayName("Apelido")]
        [Browsable(false)]
        [Required(ErrorMessage = "Por favor informe um apelido.")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "O apelido deve conter somente letras")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O apelido deve conter um mínimo de 3 caracteres.")]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        //Posição 5
        [ReadOnly(true)]
        [DisplayName("Nome completo")]
        public string FullName
        {
            get { return _firstName + ", " + _lastName; }
        }

        //Posição 6
        [DisplayName("Cargo")]
        [Required(ErrorMessage = "Por favor informe um cargo.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Last name must contain a minimum of 8 characters.")]
        public string Position
        {
            get { return _position; }
            set { _position = value; }
        }

        //Posição 7
        [DisplayName("Email")]
        [Required(ErrorMessage = "Por favor informe e-mail.")]
        [EmailAddress(ErrorMessage = "Informe e-mail válido: example@gmail.com")]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        //Posição 8
        [DisplayName("Foto")]
        public byte[] Photo
        {
            get { return _photo; }
            set { _photo = value; }

        }
        #endregion

        #region -> Métodos Públicos
        public UserModel Login(string userName, string password)
        {
            var result = _userRepository.Login(userName, password);
            if (result != null)
                return MapUserModel(result);
            else
                return null;
        }

        public bool ValidateActiveUser()
        {
            return _userRepository.ValidateActiveUser();
        }

        public int CreateUser()
        {
            UserEntity userEntity = MapUserEntity(this);
            return _userRepository.CreateUser(userEntity);
        }

        public int ModifyUser()
        {
            UserEntity userEntity = MapUserEntity(this);
            return _userRepository.ModifyUser(userEntity);
        }

        public int RemoveUser(int id)
        {
            return _userRepository.RemoveUser(id);
        }

        public UserModel GetUserById(int id)
        {
            var result = _userRepository.GetUserById(id);
            if (result != null)
                return MapUserModel(result);
            else
                return null;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            var result = _userRepository.GetAllUsers();
            return MapUserModel(result);
        }

        public UserModel RecoverPassword(string requestingUser)
        {
            var result = _userRepository.GetUserByUsername(requestingUser);
            if (result != null)
            {
                var userModel = MapUserModel(result);
                var mailService = new EmailService();
                mailService.Send(
                    recipient: userModel.Email,
                    subject: "Solicitação de recuperação de senha",
                    body: "Ola! " + userModel.FirstName + ",\nSolicitou recuperar sua senha.\n" +
                    "Sua senha atual é: " + userModel.Password + "\npor favor, mude a senha" +
                    "imediatamente para acessar a aplicacão");
                return userModel;
            }
            else
                return null;
        }
        #endregion

        #region -> Métodos Privados (Mapear dados)
        private UserEntity MapUserEntity(UserModel userModel)
        {
            var userEntity = new UserEntity
            {
                Id = userModel.Id,
                Username = userModel.Username,
                Password = userModel.Password,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Position = userModel.Position,
                Email = userModel.Email,
                Photo = userModel.Photo
            };
            return userEntity;
        }

        private UserModel MapUserModel(UserEntity userEntity)
        {
            var userModel = new UserModel()
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                Password = userEntity.Password,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Position = userEntity.Position,
                Email = userEntity.Email,
                Photo = userEntity.Photo
            };
            return userModel;
        }

        private IEnumerable<UserModel> MapUserModel(IEnumerable<UserEntity> userEntities)
        {
            var userModels = new List<UserModel>();
            foreach (var user in userEntities)
            {
                userModels.Add(MapUserModel(user));
            }
            return userModels;
        }
        #endregion
    }
}
