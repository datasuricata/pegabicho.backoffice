namespace pegabicho.shared.Arguments.Base {
    public class ResponseBase {
        private string Message { get; set; }
        public ResponseBase() => Message = "Operação realizada com sucesso.";
        public ResponseBase(string msg) => Message = msg;
    }
}
