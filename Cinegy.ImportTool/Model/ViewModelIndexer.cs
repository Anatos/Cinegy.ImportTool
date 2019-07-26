using System;
using Autofac;
using Cinegy.ImportTool.Infrastructure.Model;
using GalaSoft.MvvmLight;

namespace Cinegy.ImportTool.Model
{
    public class ViewModelIndexer : IViewModelIndexer
    {
        private readonly ILifetimeScope _scope;

        #region Constructors

        public ViewModelIndexer(ILifetimeScope scope)
        {
            _scope = scope;
        }

        #endregion

        #region Properties

        public ViewModelBase this[Type key] => (ViewModelBase)_scope.Resolve(key);

        #endregion
    }
}