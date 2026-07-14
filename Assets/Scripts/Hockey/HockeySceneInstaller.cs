using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Hockey
{
    public class HockeySceneInstaller : MonoInstaller
    {
        [SerializeField] private HockeySettings _hockeySettings;

        [SerializeField] private InputActionReference _pressAction;
        [SerializeField] private InputActionReference _pointerPositionAction;

        [SerializeField] private PuckBody _puckBody;
        [SerializeField] private TrajectoryView _trajectoryView;

        public override void InstallBindings()
        {
            Container.Bind<HockeySettings>().FromInstance(_hockeySettings).AsSingle();
            
            Container.Bind<PuckBody>().FromInstance(_puckBody).AsSingle();
            Container.Bind<TrajectoryView>().FromInstance(_trajectoryView).AsSingle();
            
            Container.Bind<TrajectoryPredictor>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PuckDragController>().AsSingle();
            
            InstallInput();
        }
        
        private void InstallInput()
        {
            Container.Bind<InputActionReference>().WithId("Press").FromInstance(_pressAction).AsCached();
            Container.Bind<InputActionReference>().WithId("PointerPosition").FromInstance(_pointerPositionAction).AsCached();

            Container.BindInterfacesAndSelfTo<HockeyInputService>().AsSingle();
        }
    }
}