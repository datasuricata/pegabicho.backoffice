﻿namespace pegabicho.backoffice.Services.Interfaces {
    public interface IViewRender {
        string Render<TModel>(string name, TModel model);
    }
}
