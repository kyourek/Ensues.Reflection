import os
import sys

import buildconfig

class Build:
    
    class ProjectNotFoundError(Exception):
        pass

    def __init__(self, config = None):
        self.config = config or buildconfig

    def project_class(self):
        return self.config.project_class

    def project_dir(self):
        
        project_dir = self.config.project_dir
        if project_dir:
            return project_dir

        script_file = os.path.abspath(sys.argv[0])
        script_path = os.path.dirname(script_file)
        
        current_path = script_path
        while True:

            items = os.listdir(current_path)
            items = [i for i in items if i.endswith('.csproj')]
            paths = [os.path.join(current_path, i) for i in items]
            files = [p for p in paths if os.path.isfile(p)]

            if len(files):
                file = files[0]
                return os.path.dirname(file)

            next_path = os.path.dirname(current_path)
            if next_path == current_path:
                raise Build.ProjectNotFoundError()

            current_path = next_path

    def process(self):

        project_dir = self.project_dir()
        project_class = self.project_class()

        project = project_class(project_dir)
        project.pack()

if __name__ == '__main__':
    Build().process()