using Moq;
using ServiceRadiusAdjuster;
using ServiceRadiusAdjuster.Model;
using ServiceRadiusAdjuster.Presenter;
using ServiceRadiusAdjuster.Service;
using ServiceRadiusAdjuster.View;
using System;
using Xunit;

namespace ServiceRadiusAdjusterTests
{
    public class OptionItemPresenterTests
    {
        [Fact(DisplayName = nameof(Constructor_AccumulationAndRadiusSameAsDefault))]
        public void Constructor_AccumulationAndRadiusSameAsDefault()
        {
            var viewMock = new Mock<IOptionItemView>();
            viewMock.SetupAllProperties();
            var model = new OptionItem(ServiceType.Building, "test", "test", 50, 50, 100f, 100f);
            var gameEngineServiceMock = new Mock<IGameEngineService>();
            var presenter = new OptionItemPresenter(viewMock.Object, model, gameEngineServiceMock.Object);

            viewMock.VerifySet(v => v.Accumulation = "50", Times.Once);
            viewMock.VerifySet(v => v.AccumulationDefault = "50", Times.Once);
            viewMock.VerifySet(v => v.Radius = "100", Times.Once);
            viewMock.VerifySet(v => v.RadiusDefault = "100", Times.Once);
            viewMock.VerifySet(v => v.ApplyButtonEnabled = false, Times.Once);
            viewMock.VerifySet(v => v.UndoButtonEnabled = false, Times.Once);
            viewMock.VerifySet(v => v.DefaultButtonEnabled = false, Times.Once);

            Assert.Equal(50, presenter.Model.Accumulation);
            Assert.Equal(50, presenter.Model.AccumulationDefault);
            Assert.Equal(100f, presenter.Model.Radius);
            Assert.Equal(100f, presenter.Model.RadiusDefault);
        }

        [Fact(DisplayName = nameof(Constructor_ValuesDifferFromDefault))]
        public void Constructor_ValuesDifferFromDefault()
        {
            var viewMock = new Mock<IOptionItemView>();
            viewMock.SetupAllProperties();
            var model = new OptionItem(ServiceType.Building, "test", "test", 10, 50, 200f, 100f);
            var gameEngineServiceMock = new Mock<IGameEngineService>();
            var presenter = new OptionItemPresenter(viewMock.Object, model, gameEngineServiceMock.Object);

            viewMock.VerifySet(v => v.Accumulation = "10", Times.Once);
            viewMock.VerifySet(v => v.AccumulationDefault = "50", Times.Once);
            viewMock.VerifySet(v => v.Radius = "200", Times.Once);
            viewMock.VerifySet(v => v.RadiusDefault = "100", Times.Once);
            viewMock.VerifySet(v => v.ApplyButtonEnabled = false, Times.Once);
            viewMock.VerifySet(v => v.UndoButtonEnabled = false, Times.Once);
            viewMock.VerifySet(v => v.DefaultButtonEnabled = true, Times.Once);

            Assert.Equal(10, presenter.Model.Accumulation);
            Assert.Equal(50, presenter.Model.AccumulationDefault);
            Assert.Equal(200f, presenter.Model.Radius);
            Assert.Equal(100f, presenter.Model.RadiusDefault);
        }

        [Fact(DisplayName = nameof(AccumulationChangedEvent))]
        public void AccumulationChangedEvent()
        {
            var viewMock = new Mock<IOptionItemView>();
            viewMock.SetupAllProperties();
            var model = new OptionItem(ServiceType.Building, "test", "test", 50, 50, 200f, 100f);
            var gameEngineServiceMock = new Mock<IGameEngineService>();
            var presenter = new OptionItemPresenter(viewMock.Object, model, gameEngineServiceMock.Object);
            viewMock.ResetCalls();

            viewMock.Object.Accumulation = "400";
            viewMock.Raise(v => v.AccumulationChanged += null, EventArgs.Empty);

            viewMock.VerifySet(v => v.Accumulation = "400", Times.Once);
            Assert.Equal("400", viewMock.Object.Accumulation);
            Assert.Equal(50, presenter.Model.Accumulation);
        }

        [Fact(DisplayName = nameof(RadiusChangedEvent))]
        public void RadiusChangedEvent()
        {
            var viewMock = new Mock<IOptionItemView>();
            viewMock.SetupAllProperties();
            var model = new OptionItem(ServiceType.Building, "test", "test", 50, 50, 200f, 100f);
            var gameEngineServiceMock = new Mock<IGameEngineService>();
            var presenter = new OptionItemPresenter(viewMock.Object, model, gameEngineServiceMock.Object);
            viewMock.ResetCalls();

            viewMock.Object.Radius = "400";
            viewMock.Raise(v => v.RadiusChanged += null, EventArgs.Empty);

            viewMock.VerifySet(v => v.Accumulation = It.IsAny<string>(), Times.Never);
            viewMock.VerifySet(v => v.Radius = "400", Times.Once);
            Assert.Equal(50, presenter.Model.Accumulation);
            Assert.Equal(200, presenter.Model.Radius);
        }

        [Fact(DisplayName = nameof(Apply_AfterRadiusChanged))]
        public void Apply_AfterRadiusChanged()
        {
            var viewMock = new Mock<IOptionItemView>();
            viewMock.SetupAllProperties();
            var model = new OptionItem(ServiceType.Building, "test", "test", 50, 50, 100f, 100f);
            var gameEngineServiceMock = new Mock<IGameEngineService>();
            gameEngineServiceMock.Setup(vm => vm.ApplyToGame(It.IsAny<OptionItem>())).Returns(Result.Ok());
            var presenter = new OptionItemPresenter(viewMock.Object, model, gameEngineServiceMock.Object);
            viewMock.ResetCalls();

            viewMock.Object.Radius = "200";
            viewMock.Raise(v => v.RadiusChanged += null, EventArgs.Empty);
            presenter.Apply();

            viewMock.VerifySet(v => v.Accumulation = It.IsAny<string>(), Times.Never);
            viewMock.VerifySet(v => v.Radius = "200", Times.Once());

            Assert.Equal(50, presenter.Model.Accumulation);
            Assert.Equal(200, presenter.Model.Radius);
        }

        [Fact(DisplayName = nameof(Default_AfterRadiusChanged))]
        public void Default_AfterRadiusChanged()
        {
            var viewMock = new Mock<IOptionItemView>();
            viewMock.SetupAllProperties();
            var model = new OptionItem(ServiceType.Building, "test", "test", 50, 50, 100f, 100f);
            var gameEngineServiceMock = new Mock<IGameEngineService>();
            gameEngineServiceMock.Setup(vm => vm.ApplyToGame(It.IsAny<OptionItem>())).Returns(Result.Ok());
            var presenter = new OptionItemPresenter(viewMock.Object, model, gameEngineServiceMock.Object);
            viewMock.ResetCalls();

            viewMock.Object.Radius = "200";
            viewMock.Raise(v => v.RadiusChanged += null, EventArgs.Empty);
            presenter.DefaultAndApply();

            viewMock.VerifySet(v => v.Radius = "100", Times.Once());
            Assert.Equal(100, presenter.Model.Radius);
        }

        [Fact(DisplayName = nameof(PersistsNewValueAfterApply))]
        public void PersistsNewValueAfterApply()
        {
            var viewMock = new Mock<IOptionItemView>();
            viewMock.SetupAllProperties();
            var model = new OptionItem(ServiceType.Building, "test", "test", 50, 50, 200f, 100f);
            var gameEngineServiceMock = new Mock<IGameEngineService>();
            gameEngineServiceMock.Setup(vm => vm.ApplyToGame(It.IsAny<OptionItem>())).Returns(Result.Ok());
            var presenter = new OptionItemPresenter(viewMock.Object, model, gameEngineServiceMock.Object);
            viewMock.ResetCalls();

            viewMock.Object.Radius = "400";
            viewMock.Raise(v => v.RadiusChanged += null, EventArgs.Empty);

            presenter.Apply();

            viewMock.Object.Radius = "800";
            viewMock.Raise(v => v.RadiusChanged += null, EventArgs.Empty);

            presenter.Undo();

            Assert.Equal(400, presenter.Model.Radius);
            Assert.Equal("400", viewMock.Object.Radius);
        }

        [Fact(DisplayName = nameof(RadiusChanged_IsDifferentFromBefore_ApplyAndUndoButtonsEnabled))]
        public void RadiusChanged_IsDifferentFromBefore_ApplyAndUndoButtonsEnabled()
        {
            var viewMock = new Mock<IOptionItemView>();
            viewMock.SetupAllProperties();
            var model = new OptionItem(ServiceType.Building, "test", "test", 50, 50, 200f, 100f);
            var gameEngineServiceMock = new Mock<IGameEngineService>();
            var presenter = new OptionItemPresenter(viewMock.Object, model, gameEngineServiceMock.Object);
            viewMock.ResetCalls();

            viewMock.Object.Radius = "400";
            viewMock.Raise(v => v.RadiusChanged += null, EventArgs.Empty);

            viewMock.VerifySet(v => v.ApplyButtonEnabled = true);
            viewMock.VerifySet(v => v.UndoButtonEnabled = true);
        }

        [Fact(DisplayName = nameof(RadiusChanged_IsSameAsBefore_ButtonState))]
        public void RadiusChanged_IsSameAsBefore_ButtonState()
        {
            var viewMock = new Mock<IOptionItemView>();
            viewMock.SetupAllProperties();
            var model = new OptionItem(ServiceType.Building, "test", "test", 50, 50, 200f, 100f);
            var gameEngineServiceMock = new Mock<IGameEngineService>();
            var presenter = new OptionItemPresenter(viewMock.Object, model, gameEngineServiceMock.Object);
            viewMock.ResetCalls();

            viewMock.Object.Radius = "400";
            viewMock.Raise(v => v.RadiusChanged += null, EventArgs.Empty);
            viewMock.Object.Radius = "200";
            viewMock.Raise(v => v.RadiusChanged += null, EventArgs.Empty);

            Assert.False(viewMock.Object.ApplyButtonEnabled);
            Assert.False(viewMock.Object.UndoButtonEnabled);
        }

        [Fact(DisplayName = nameof(RadiusChanged_Apply_Undo_SameAsBefore))]
        public void RadiusChanged_Apply_Undo_SameAsBefore()
        {
            var viewMock = new Mock<IOptionItemView>();
            viewMock.SetupAllProperties();
            var model = new OptionItem(ServiceType.Building, "test", "test", 50, 50, 200f, 100f);
            var gameEngineServiceMock = new Mock<IGameEngineService>();
            gameEngineServiceMock.Setup(vm => vm.ApplyToGame(It.IsAny<OptionItem>())).Returns(Result.Ok());
            var presenter = new OptionItemPresenter(viewMock.Object, model, gameEngineServiceMock.Object);
            viewMock.ResetCalls();

            viewMock.Object.Radius = "400";
            viewMock.Raise(v => v.RadiusChanged += null, EventArgs.Empty);

            presenter.Apply();
            presenter.Undo();

            Assert.Equal("400", presenter.View.Radius);
            Assert.Equal(400, presenter.Model.Radius);
            Assert.False(viewMock.Object.ApplyButtonEnabled);
            Assert.False(viewMock.Object.UndoButtonEnabled);
        }

        [Fact(DisplayName = nameof(Apply_RadiusSameAsDefault_DefaultButtonDisabled))]
        public void Apply_RadiusSameAsDefault_DefaultButtonDisabled()
        {
            var viewMock = new Mock<IOptionItemView>();
            viewMock.SetupAllProperties();
            var model = new OptionItem(ServiceType.Building, "test", "test", 50, 50, 200f, 100f);
            var gameEngineServiceMock = new Mock<IGameEngineService>();
            gameEngineServiceMock.Setup(vm => vm.ApplyToGame(It.IsAny<OptionItem>())).Returns(Result.Ok());
            var presenter = new OptionItemPresenter(viewMock.Object, model, gameEngineServiceMock.Object);
            viewMock.ResetCalls();

            viewMock.Object.Radius = "100";
            viewMock.Raise(v => v.RadiusChanged += null, EventArgs.Empty);

            presenter.Apply();

            Assert.False(viewMock.Object.DefaultButtonEnabled);
        }

        [Fact(DisplayName = nameof(Undo_AfterAccumulationChanged))]
        public void Undo_AfterAccumulationChanged()
        {
            var viewMock = new Mock<IOptionItemView>();
            viewMock.SetupAllProperties();
            var model = new OptionItem(ServiceType.Building, "test", "test", 50, 50, 200f, 100f);
            var gameEngineServiceMock = new Mock<IGameEngineService>();
            var presenter = new OptionItemPresenter(viewMock.Object, model, gameEngineServiceMock.Object);
            viewMock.ResetCalls();

            viewMock.Object.Accumulation = "400";
            viewMock.Raise(v => v.AccumulationChanged += null, EventArgs.Empty);

            presenter.Undo();

            viewMock.VerifySet(v => v.Accumulation = "400", Times.Once);
            viewMock.VerifySet(v => v.Accumulation = "50", Times.Once);
            Assert.Equal("50", viewMock.Object.Accumulation);
            Assert.Equal(50, model.Accumulation);
        }

        [Fact(DisplayName = nameof(Undo_AfterRadiusChanged))]
        public void Undo_AfterRadiusChanged()
        {
            var viewMock = new Mock<IOptionItemView>();
            viewMock.SetupAllProperties();
            var model = new OptionItem(ServiceType.Building, "test", "test", 50, 50, 200f, 100f);
            var gameEngineServiceMock = new Mock<IGameEngineService>();
            var presenter = new OptionItemPresenter(viewMock.Object, model, gameEngineServiceMock.Object);
            viewMock.ResetCalls();

            viewMock.Object.Radius = "400";
            viewMock.Raise(v => v.RadiusChanged += null, EventArgs.Empty);

            presenter.Undo();

            viewMock.VerifySet(v => v.Radius = "200", Times.Once);
            Assert.Equal("200", viewMock.Object.Radius);
            Assert.Equal(200, model.Radius);
        }

        [Fact(DisplayName = nameof(Undo_AfterAccumulationAndRadiusChanged))]
        public void Undo_AfterAccumulationAndRadiusChanged()
        {
            var viewMock = new Mock<IOptionItemView>();
            viewMock.SetupAllProperties();
            var model = new OptionItem(ServiceType.Building, "test", "test", 50, 50, 200f, 100f);
            var gameEngineServiceMock = new Mock<IGameEngineService>();
            var presenter = new OptionItemPresenter(viewMock.Object, model, gameEngineServiceMock.Object);
            viewMock.ResetCalls();

            viewMock.Object.Accumulation = "60";
            viewMock.Raise(v => v.AccumulationChanged += null, EventArgs.Empty);

            viewMock.Object.Radius = "210";
            viewMock.Raise(v => v.RadiusChanged += null, EventArgs.Empty);

            presenter.Undo();

            viewMock.VerifySet(v => v.Accumulation = "60", Times.Once);
            viewMock.VerifySet(v => v.Accumulation = "50", Times.Once);
            viewMock.VerifySet(v => v.Radius = "210", Times.Once);
            viewMock.VerifySet(v => v.Radius = "200", Times.Once);
            Assert.Equal("50", viewMock.Object.Accumulation);
            Assert.Equal(50, model.Accumulation);
            Assert.Equal("200", viewMock.Object.Radius);
            Assert.Equal(200, model.Radius);
        }
    }
}
